using Data.cs.Entities.Seguridad;
using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Interfaces.Seguridad
{
    public class IConfiguracionesRepositorio
    {
        Task<Response<List<ElementoSrcModelo>>> ObtenerElementosSistema();
        Task<Response<Configuracion>> DSave(Configuracion newItem);
        Task<Response<Configuracion>> DObtenerConfiguracion();
        Task<Response<bool>> DUpdate(Configuracion entity);
    }
}
