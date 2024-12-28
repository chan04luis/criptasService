using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Seguridad
{
    public interface IBusModulo
    {
        Task<Response<ModuloModelo>> BCreate(ModuloModelo createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<ModuloModelo>> BGet(Guid iKey);
        Task<Response<List<ModuloModelo>>> BGetAll();
        Task<Response<ModuloModelo>> BUpdate(ModuloModelo updateModel);
    }
}
