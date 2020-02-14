begin
declare @viewName nvarchar(200) =N'v_Parts';
declare @viewScript nvarchar(max) =N'with v1 as(
select 
 c.*,
 d.DeviceTypeCode Code,
 d.ModelCode Model 
from Parts c inner join Devices d on c.DeviceId = d.Id)
,v2 as(
select
c.*,
d.Code,
d.Model 
from DeviceTypeParts c inner join DeviceTypes d on c.DeviceTypeId = d.Id)
select 
		t.[Id]
      ,t.[DeviceId]
      ,t.PartCode
      ,c.PartName
      ,c.PartType
      ,c.HasStatus
      ,ISNULL(t.[PortNumber],c.PortNumber)PortNumber
      ,ISNULL(t.CompanyId,c.CompanyId)CompanyId
      ,ISNULL(t.OprationId,c.OprationId)OprationId
      ,ISNULL(t.BrandId,c.BrandId)BrandId
      ,case when t.[Warranty] = 0 or t.Warranty < 0 then c.Warranty else t.Warranty end Warranty
      ,ISNULL(t.[Enabled],c.[Enabled])[Enabled]
      ,t.InstallTime     
      ,t.Status
      ,t.AlarmStatus
from
 v1 t left join v2 c on t.Code = c.Code and t.Model = c.Model and t.PartCode = c.PartCode'

IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].['+@viewName+']'))
set @viewScript = N'create view '+@viewName +' as ' + @viewScript;
else
set @viewScript = N'alter view '+@viewName+' as ' + @viewScript;

EXEC sp_executesql @viewScript
 end
