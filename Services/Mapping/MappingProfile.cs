using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domains.Entities;

namespace Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Ordenes, OrdenesDTO>()
           .ForMember(dest => dest.Operacion, opt => opt.MapFrom(src => (OperacionEnum)src.Operacion))
           .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => (EstadosEnum)src.Estado))
           .ReverseMap()
           .ForMember(dest => dest.Operacion, opt => opt.MapFrom(src => (char)src.Operacion))
           .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => (int)src.Estado));

            CreateMap<Activos, ActivosDTO>().ReverseMap()
            .ForMember(dest => dest.TipoActivo, opt => opt.MapFrom(src => (TiposActivosEnum)src.TipoActivo))
            .ReverseMap()
            .ForMember(dest => dest.TipoActivo, opt => opt.MapFrom(src => (int)src.TipoActivo));           
        }
    }
}
