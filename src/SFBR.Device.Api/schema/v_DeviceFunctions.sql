begin
declare @viewName nvarchar(200) =N'v_DeviceFunctions';
declare @viewScript nvarchar(max) =N'with v1 as(
select 
 c.*,
 d.DeviceTypeCode Code,
 d.ModelCode Model 
from DeviceFunctions c inner join Devices d on c.DeviceId = d.Id)
,v2 as(
select
c.*,
d.Code,
d.Model 
from DeviceTypeFunctions c inner join DeviceTypes d on c.DeviceTypeId = d.Id)
select 
		t.[Id]
      ,t.[DeviceId]
      ,t.FunctionCode
      ,t.SettingTypeName
      ,t.CallbackCodes
      ,c.FunctionName
      ,c.FunctionType
      ,ISNULL(t.[PortNumber],c.PortNumber)PortNumber
      ,ISNULL(t.Setting,c.Setting)Setting
      ,ISNULL(t.Sort,c.Sort)LowerValue
      ,ISNULL(t.[Enabled],c.[Enabled])[Enabled]
      ,t.LockSetting     
      ,c.Description
	  ,c.Sort
from
 v1 t left join v2 c on t.Code = c.Code and t.Model = c.Model and t.FunctionCode = c.FunctionCode'

IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].['+@viewName+']'))
set @viewScript = N'create view '+@viewName +' as ' + @viewScript;
else
set @viewScript = N'alter view '+@viewName+' as ' + @viewScript;

EXEC sp_executesql @viewScript
 end
