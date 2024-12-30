using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusBoton
    {
        Task<Response<BotonModelo>> BCreate(BotonModelo createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<BotonModelo>> BGet(Guid iKey);
        Task<Response<List<BotonModelo>>> BGetAll();
        Task<Response<BotonModelo>> BUpdate(BotonModelo updateModel);
    }
}
