using Newtonsoft.Json;
using System;
namespace HC.LegacySync.Application
{
[Serializable]
public class DeleteASurveyCMD : HC.Command
	{
	[JsonConstructor]
	public DeleteASurveyCMD(
	Guid tenantUniqueId, 
	Guid correlationUniqueId, 
	Guid userUniqueId,
	long version,
	Guid uniqueId
	) : base (tenantUniqueId, correlationUniqueId, userUniqueId , version)
	{
		UniqueId = uniqueId
	}
	public Guid UniqueId {get;set;}
	}
}

