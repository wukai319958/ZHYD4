begin
declare @viewName nvarchar(200) =N'v_terminal';
declare @viewScript nvarchar(max) =N' with terminal as( select t.* from Devices t where t.DeviceTypeCode = ''terminal'')
select terminal.*
,Locations.Latitude
,Locations.Longitude
,case when  Locations.Latitude = 0 and Locations.Longitude = 0 then 0 else 1 end  IsPosition
,Regions.RegionCode
,Regions.RegionName
,(select count(1) from DeviceAlarms a where a.DeviceId = terminal.Id and a.Status >''0'' and a.Status <''5'') AlarmCount
from terminal left join Locations on terminal.LocationId = Locations.Id
left join Regions on terminal.RegionId = regions.Id'

IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].['+@viewName+']'))
set @viewScript = N'create view '+@viewName +' as ' + @viewScript;
else
set @viewScript = N'alter view '+@viewName+' as ' + @viewScript;

EXEC sp_executesql @viewScript
 end