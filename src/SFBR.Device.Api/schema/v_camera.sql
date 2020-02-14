begin
declare @viewName nvarchar(200) =N'v_camera';
declare @viewScript nvarchar(max) =N'select * from Devices t
where t.DeviceTypeCode = ''camera'''

IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].['+@viewName+']'))
set @viewScript = N'create view '+@viewName +' as ' + @viewScript;
else
set @viewScript = N'alter view '+@viewName+' as ' + @viewScript;

EXEC sp_executesql @viewScript
 end