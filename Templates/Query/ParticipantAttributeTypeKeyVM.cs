using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using HC.Core.Domain.Aggregate.ParticipantAttributeTypeKey;
using Newtonsoft.Json;

namespace HC.Core.Application.Models.ViewModel.ParticipantAttributeTypeKey
{
    public class ParticipantAttributeTypeKeyVM : IViewModel, IObjectMapper
    {
        [DataMember]
        public Guid UniqueId { get; set; }

        [DataMember]
        [JsonConverter(typeof(VersionConverter))]
        public long Version { get; set; }

    

        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<ParticipantAttributeTypeKeyAR, ParticipantAttributeTypeKeyVM>()
                .ForMember(l => l.UniqueId, opt => opt.MapFrom(l => l.UniqueId.Value));                
        }
    }
}
