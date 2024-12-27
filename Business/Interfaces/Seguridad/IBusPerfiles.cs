using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Seguridad
{
    public interface IBusPerfiles
    {
        Task<Response<PerfilModelo>> BCreate(PerfilModelo createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<PerfilModelo>> BGet(Guid iKey);
        Task<Response<List<PerfilModelo>>> BGetAll();
        Task<Response<PerfilModelo>> BUpdate(PerfilModelo updateModel);
    }
}
