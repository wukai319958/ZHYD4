begin
declare @viewScript nvarchar(max) = N'view [dbo].[v_Devices] as
select 
		d.[Id]
      ,d.[EquipNum]
      ,d.[DeviceTypeCode]
      ,d.[ModelCode]
      ,d.[IsMaster]
      ,d.[DeviceIP]
      ,d.[DevicePort]
      ,d.[ServerIP]
      ,d.[ServerPort]
      ,d.[ParentId]
      ,d.[Enabled]
      ,d.[Connection]
      ,d.[CreationTime]
      ,d.[DeviceName]
      ,d.[TentantId]
      ,d.[Description]
      ,ISNULL(d.[CompanyId],t.[CompanyId])[CompanyId]
      ,ISNULL(d.[OprationId],t.[OprationId])[OprationId]
      ,ISNULL(d.[BrandId],t.[BrandId])[BrandId]
      ,case when d.[Warranty] = 0 or d.Warranty < 0 then t.Warranty else d.Warranty end Warranty
      ,d.[InstallTime]
      ,d.[PortNumber]
      ,d.[TerminalId]
      ,d.[RegionId]
      ,d.[LocationId] 
from Devices d left join DeviceTypes t
on d.DeviceTypeCode = t.Code and d.ModelCode = t.Model'
IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_Devices]'))
set @viewScript = N'create ' + @viewScript;
 
else
set @viewScript = N'alter ' + @viewScript;

EXEC sp_executesql @viewScript
end



