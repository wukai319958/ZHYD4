begin
declare @viewName nvarchar(200) =N'v_DeviceAlarms';
declare @viewScript nvarchar(max) =N'with v1 as(
select 
 c.*,
 d.DeviceTypeCode Code,
 d.ModelCode Model 
from dbo.DeviceAlarms c inner join Devices d on c.DeviceId = d.Id)
,v2 as(
select
c.*,
d.Code,
d.Model 
from dbo.DeviceTypeAlarms c inner join DeviceTypes d on c.DeviceTypeId = d.Id)
select 
		t.[Id]
      ,t.[DeviceId]
      ,t.AlarmCode
      ,t.TargetCode
      ,c.AlarmName
      ,c.GroupName
      ,c.AlarmType
      ,c.AlarmFrom
      ,c.AlarmLevel
      ,c.NormalValue
      ,c.AutoSendOrder
      ,c.IsStatistics
      ,c.AlarmingDescription
      ,c.AlarmedDescription
      ,c.SendAlarmingMessage
      ,c.SendAlarmedMessage
      ,c.CallAlarmingPhone
      ,c.CallAlarmedPhone
      ,c.SendAlarmingEmail
      ,c.SendAlarmedEmail
      ,ISNULL(t.RepairTime,c.RepairTime)RepairTime
      ,ISNULL(t.[Enabled],c.[Enabled])[Enabled]
      ,t.Status     
from
 v1 t left join v2 c on t.Code = c.Code and t.Model = c.Model and t.AlarmCode = c.AlarmCode'

IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].['+@viewName+']'))
set @viewScript = N'create view '+@viewName +' as ' + @viewScript;
else
set @viewScript = N'alter view '+@viewName+' as ' + @viewScript;

EXEC sp_executesql @viewScript
 end
