using Models.Seguridad;
using Utils;

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
