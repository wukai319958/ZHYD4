begin
declare @viewName nvarchar(200) =N'v_Controllers';
declare @viewScript nvarchar(max) =N'with v1 as(
select 
 c.*,
 d.DeviceTypeCode Code,
 d.ModelCode Model 
from Controllers c inner join Devices d on c.DeviceId = d.Id)
,v2 as(
select
c.*,
d.Code,
d.Model 
from DeviceTypeControllers c inner join DeviceTypes d on c.DeviceTypeId = d.Id)
select 
		t.[Id]
      ,t.[DeviceId]
      ,t.ControllerCode
      ,ISNULL(t.[PortNumber],c.PortNumber)PortNumber
      ,ISNULL(t.Buttons,c.Buttons)Buttons
      ,ISNULL(t.[Enabled],c.[Enabled])[Enabled]
      ,t.ControllerStatus     
      ,t.[Description]     
from
 v1 t left join v2 c on t.Code = c.Code and t.Model = c.Model and t.ControllerCode = c.ControllerCode'

IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].['+@viewName+']'))
set @viewScript = N'create view '+@viewName +' as ' + @viewScript;
else
set @viewScript = N'alter view '+@viewName+' as ' + @viewScript;

EXEC sp_executesql @viewScript
 end
