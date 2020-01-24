using Newtonsoft.Json;
using System;
namespace HC.Isaac.Application
{
[Serializable]
public class DeleteAPosWordStringCMD : HC.Command
	{
	[JsonConstructor]
	public DeleteAPosWordStringCMD(
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

