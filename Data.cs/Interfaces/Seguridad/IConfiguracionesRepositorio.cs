using Data.cs.Entities.Seguridad;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IConfiguracionesRepositorio
    {
        Task<Response<List<ElementoSrcModelo>>> ObtenerElementosSistema();
        Task<Response<Configuracion>> DSave(Configuracion newItem);
        Task<Response<Configuracion>> DObtenerConfiguracion();
        Task<Response<bool>> DUpdate(Configuracion entity);
    }
}
