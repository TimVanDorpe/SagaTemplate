





// This is generated code:
using Newtonsoft.Json;
using System;
namespace HC.Isaac.Application
{
[Serializable]
public class GenerateTokenCMD : HC.Command
	{
	[JsonConstructor]
	public GenerateTokenCMD(
	Guid tenantUniqueId, 
	Guid correlationUniqueId, 
	Guid userUniqueId
	,string name
	,string password
	,int tim
	) : base (tenantUniqueId, correlationUniqueId, userUniqueId)
	{
		this.Name = name;
		this.Password = password;
		this.Tim = tim;
	}

	public string Name {get;set;}
	public string Password {get;set;}
	public int Tim {get;set;}
	}
}




