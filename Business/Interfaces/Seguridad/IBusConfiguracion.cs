using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Seguridad
{
    public interface IBusConfiguracion
    {
        Task<Response<List<ModuloModelo>>> ObtenerElementosSistema();
        Task<Response<bool>> bCrearConfiguracion(ConfiguracionModelo createModel);
        Task<Response<ConfiguracionModelo>> BObtenerConfiguracion();
    }
}
