using Newtonsoft.Json;
using System;
namespace HC.Isaac.Application
{
[Serializable]
public class DeletePosWordStringCMD : HC.Command
	{
	[JsonConstructor]
	public DeletePosWordStringCMD(
	Guid tenantUniqueId, 
	Guid correlationUniqueId, 
	Guid userUniqueId,
	long version,
	Guid uniqueId
	) : base (tenantUniqueId, correlationUniqueId, userUniqueId , version)
	{
		this.UniqueId = uniqueId
	}
	public Guid UniqueId {get;set;}
	}
}

