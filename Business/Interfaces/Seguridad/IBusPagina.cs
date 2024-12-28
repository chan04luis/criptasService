using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Seguridad
{
    public interface IBusPagina
    {
        Task<Response<PaginaModelo>> BCreate(PaginaModelo createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<PaginaModelo>> BGet(Guid iKey);
        Task<Response<List<PaginaModelo>>> BGetAll();
        Task<Response<PaginaModelo>> BUpdate(PaginaModelo updateModel);
    }
}
