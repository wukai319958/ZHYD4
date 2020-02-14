begin
declare @viewName nvarchar(200) =N'v_Channels';
declare @viewScript nvarchar(max) =N'with channel as(
select 
 c.*,
 d.DeviceTypeCode Code,
 d.ModelCode Model 
from Channels c inner join Devices d on c.DeviceId = d.Id)
,channelConfig as(
select
c.*,
d.Code,
d.Model 
from DeviceTypeChannels c inner join DeviceTypes d on c.DeviceTypeId = d.Id)
select 
		t.[Id]
      ,t.[DeviceId]
      ,c.PortType
      ,ISNULL(t.[PortNumber],c.PortNumber)PortNumber
      ,ISNULL(t.[PortDefaultName],c.[PortDefaultName])[PortDefaultName]
      ,ISNULL(t.[OutputType],c.[OutputType])[OutputType]
      ,ISNULL(t.[OutputThreePhase],c.[OutputThreePhase])[OutputThreePhase]
      ,ISNULL(t.[OutputValue],c.[OutputValue])[OutputValue]
      ,ISNULL(t.[Enabled],c.[Enabled])[Enabled]
      ,ISNULL(t.[Sort],c.[Sort])[Sort]    
      ,t.[Description]     
from
 channel t left join channelConfig c on t.Code = c.Code and t.Model = c.Model and t.PortNumber = c.PortNumber'

IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].['+@viewName+']'))
set @viewScript = N'create view '+@viewName +' as ' + @viewScript;
else
set @viewScript = N'alter view '+@viewName+' as ' + @viewScript;

EXEC sp_executesql @viewScript
 end