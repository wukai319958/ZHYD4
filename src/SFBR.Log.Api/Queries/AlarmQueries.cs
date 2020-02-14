using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SFBR.Log.Api.ViewModel;
using Dapper;
using Microsoft.Extensions.Primitives;
using SFBR.Log.Api.Infrastructure;

namespace SFBR.Log.Api.Queries
{
    public class AlarmQueries : IAlarmQueries, IDisposable
    {
        private readonly IDbConnection _connection;
        public AlarmQueries(IDbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State == ConnectionState.Closed) _connection.Open();
        }

        public async Task<AlarmClearCount> GetAlarmCountByDisposeStatus()
        {
            string sqltext = @"SELECT 
                                  SUM(CASE WHEN alarm.IsClear=0 THEN 1 ELSE 0 END) ClearNum
                                 ,SUM(CASE WHEN alarm.IsClear=1 THEN 1 ELSE 0 END) UnClearNum
                               FROM 
                                  AlarmLogs AS alarm";
            var result = await _connection.QueryFirstAsync<AlarmClearCount>(sqltext);
            return result;
        }

        public async Task<IEnumerable<AlarmLevelCount>> GetAlarmCountByLevel()
        {
            string sqltext = "SELECT AlarmLevel AS Name,Count(*) AS [Count] FROM ALarmLogs GROUP BY AlarmLevel";
            var alarmLevelResult = await _connection.QueryAsync<AlarmLevelCount>(sqltext);
            return alarmLevelResult;
        }

        public async Task<PageResult<AlarmMdoel>> GetAlarms(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows)
        {
            try
            {
                string sqltext = @"SELECT 
                                   a.*,
                                   d.DeviceName
                                   ,d.DeviceTypeCode
                                   ,d.ModelCode
                                   ,d.RegionId
                                   ,d.TentantId
                                   FROM AlarmLogs AS a
                                   LEFT JOIN Devices AS d
                                   ON a.EquipNum=d.EquipNum
                                   WHERE 1=1 ";
                var condition = query.GetWhereToParString();
                sqltext += string.IsNullOrEmpty(condition.Item1) ? "" : $"and {condition.Item1}";
                var alarms = await _connection.PageingAsync<AlarmMdoel>(sqltext, page, rows, param: condition.Item2);
                return alarms;
            }
            catch(Exception E)
            {
                return null;
            }
        }

        public async Task<IEnumerable<AlarmDealTime>> GetAlarmTopTime()
        {
            string sqltext = @"SELECT Top 5
                               DATEDIFF(HOUR,t.AlarmTime,t.ClearTime) AS ActualTime
                               ,t.DeviceName
                               ,t.RepairTime
                               FROM
                               (SELECT  a.RepairTime,a.ClearTime,a.AlarmTime,d.DeviceName,a.EquipNum
                               FROM dbo.AlarmLogs AS a
                               LEFT JOIN dbo.Devices AS d
                               ON a.EquipNum=d.EquipNum
                               WHERE a.IsClear = 1) AS t
                               ORDER BY ActualTime DESC";
            var alarms = await _connection.QueryAsync<AlarmDealTime>(sqltext);
            var result = alarms.ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Number = i + 1;
            }
            return result;
        }


        #region
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        ~AlarmQueries()
        {
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // 清理托管资源
                _connection.Dispose();
            }
            //让类型知道自己已经被释放
            disposed = true;
        }



        #endregion

    }
}
