using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APID.Dtos;
using AutoMapper;
using Core.Entities;

namespace APID.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Genero, GeneroDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Personaje, PersonajeDto>().ReverseMap();
            CreateMap<VideoFilm, VideoFilmDto>().ReverseMap().ForMember(dst => dst.PersonajesAsociados, opt => opt.Ignore()) ;
        }
    }
}