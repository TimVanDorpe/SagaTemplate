
// This is generated code:
using Newtonsoft.Json;
using System;
namespace HC.Isaac.Application
{
[Serializable]
public class CreateAPosWordStringCMD : HC.Command
	{
	[JsonConstructor]
	public CreateAPosWordStringCMD(
	Guid tenantUniqueId, 
	Guid correlationUniqueId, 
	Guid userUniqueId
	,Text text
	,LanguageVO language
	,PosPolarityENUM defaultpospolarity
	) : base (tenantUniqueId, correlationUniqueId, userUniqueId)
	{
		this.Text = text;
		this.Language = language;
		this.DefaultPosPolarity = defaultpospolarity;
	}

	public Text Text {get;set;}
	public LanguageVO Language {get;set;}
	public PosPolarityENUM DefaultPosPolarity {get;set;}
	}
}

