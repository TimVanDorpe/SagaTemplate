
using System;
using System.Runtime.Serialization;
using AutoMapper;
using HC.Isaac.Domain.Aggregate.PosWordString;
using Newtonsoft.Json;

namespace HC.Isaac.Application.Models.ViewModel.PosWordString
{
    public class PosWordStringVM : IViewModel, IObjectMapper
    {
        [DataMember]
        public Guid UniqueId { get; set; }        
      

		[DataMember]
		public Text Text {get;set;}

		[DataMember]
		public LanguageVO Language {get;set;}

		[DataMember]
		public PosPolarityENUM DefaultPosPolarity {get;set;}
       

        [DataMember]
        [JsonConverter(typeof(VersionConverter))]
        public long Version { get; set; }
		     
	    // TODO : check the mapping (most likely add .Value)
        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<PosWordStringAR, PosWordStringVM>()
                .ForMember(f => f.Version, opt => opt.MapFrom(z => z.Version))
                .ForMember(f => f.UniqueId, opt => opt.MapFrom(z => z.UniqueId.Value))
			   .ForMember(f => f.Text, opt => opt.MapFrom(z => z.Text))
			   .ForMember(f => f.Language, opt => opt.MapFrom(z => z.Language))
			   .ForMember(f => f.DefaultPosPolarity, opt => opt.MapFrom(z => z.DefaultPosPolarity))
              ;
        }
    }
}
