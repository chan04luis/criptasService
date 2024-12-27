using AutoMapper;
using Data.cs.Entities.Seguridad;
using Modelos.Seguridad;
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
            CreateMap<Perfil, PerfilModelo>().ReverseMap();
        }
    }
}
