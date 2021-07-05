using AutoMapper;
using System;
using Urna.Domain.Domain;
using Urna.Entity.Entity;

namespace Urna.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidatoModel, Candidato>();
            CreateMap<Candidato, CandidatoModel>();
            CreateMap<VotoModel, Voto>();
            CreateMap<Voto, VotoModel>();
            CreateMap<Guid, int>();
            CreateMap<int, Guid>();
        }
    }
}
