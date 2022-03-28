
// This is generated code:
using Newtonsoft.Json;
using System;
namespace HC.LegacySync.Application
{
[Serializable]
public class CreateASurveyCMD : HC.Command
	{
	[JsonConstructor]
	public CreateASurveyCMD(
	Guid tenantUniqueId, 
	Guid correlationUniqueId, 
	Guid userUniqueId
	,long version
	,Guid touchpointuniqueid
	) : base (tenantUniqueId, correlationUniqueId, userUniqueId , version)
	{
		TouchpointUniqueId = touchpointuniqueid;
	}

	public Guid TouchpointUniqueId {get;set;}
	}
}

