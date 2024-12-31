using AutoMapper;
using Data.cs.Entities.Seguridad;
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

            CreateMap<Boton, BotonModelo>().ReverseMap();
            CreateMap<Perfil, PerfilModelo>().ReverseMap();

            CreateMap<Usuarios, UsuarioModelo>().ReverseMap();

            CreateMap<Boton, BotonModelo>().ReverseMap();

            CreateMap<Perfil, PerfilModelo>().ReverseMap();

            CreateMap<Pagina, PaginaModelo>().ReverseMap();

            CreateMap<Modulo, ModuloModelo>().ReverseMap();

            CreateMap<Configuracion, ConfiguracionModelo>().ReverseMap();

        }
    }
}
