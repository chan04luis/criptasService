using AutoMapper;
using Data.cs.Entities.Seguridad;
using Models.Models;
using Models.Request.Seguridad;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappers
{
    public class SeguridadMapperProfile:Profile
    {
        public SeguridadMapperProfile()
        {
            MapearSeguridad();
        }

        private void MapearSeguridad()
        {
            CreateMap<Perfil, PerfilModelo>()
                .ForMember(dest => dest.IdPerfil, opt => opt.MapFrom(src => src.id)).ReverseMap();

            CreateMap<PerfilRequest, Perfil>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.IdPerfil)).ReverseMap();

            CreateMap<Usuarios, UsuarioModelo>().ReverseMap();
            CreateMap<EntUsuarios, UsuarioModelo>().ReverseMap();

            CreateMap<Boton, BotonModelo>().ReverseMap();
            CreateMap<Boton, BotonRequest>().ReverseMap();

            CreateMap<Pagina, PaginaModelo>().ReverseMap();
            CreateMap<Pagina, PaginaRequest>().ReverseMap();

            CreateMap<Modulo, ModuloModelo>().ReverseMap();
            CreateMap<Modulo, ModuloRequest>().ReverseMap();

            CreateMap<Configuracion, ConfiguracionModelo>().ReverseMap();

        }
    }
}
