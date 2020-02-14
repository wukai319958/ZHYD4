using Microsoft.Extensions.Primitives;
using SFBR.Log.Api.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.Queries
{
    public interface IAlarmQueries
    {
        Task<PageResult<AlarmMdoel>> GetAlarms(IEnumerable<KeyValuePair<string, StringValues>> query, int page, int rows);
        Task<IEnumerable<AlarmDealTime>> GetAlarmTopTime();
        Task<AlarmClearCount> GetAlarmCountByDisposeStatus();

        Task<IEnumerable<AlarmLevelCount>> GetAlarmCountByLevel();
    }
}
