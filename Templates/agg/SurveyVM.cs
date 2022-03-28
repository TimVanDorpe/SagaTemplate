
using System;
using System.Runtime.Serialization;
using AutoMapper;
using HC.LegacySync.Domain.Aggregate.Survey;
using Newtonsoft.Json;

namespace HC.LegacySync.Application.Models.ViewModel.Survey
{
   [Serializable]
    public class SurveyVM : IViewModel, IObjectMapper
    {
        [DataMember]
        public Guid UniqueId { get; set; }        
      

		[DataMember]
		public Guid TouchpointUniqueId {get;set;}
       

        [DataMember]
        [JsonConverter(typeof(VersionConverter))]
        public long Version { get; set; }
		     
	    // TODO : check the mapping (most likely add .Value)
        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<SurveyAR, SurveyVM>()
                .ForMember(f => f.Version, opt => opt.MapFrom(z => z.Version))
                .ForMember(f => f.UniqueId, opt => opt.MapFrom(z => z.UniqueId.Value))
			   .ForMember(f => f.TouchpointUniqueId, opt => opt.MapFrom(z => z.TouchpointUniqueId))
              ;
        }
    }
}
