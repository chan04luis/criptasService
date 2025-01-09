using Models.Request.Seguridad;
using Models.Seguridad;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusPagina
    {
        Task<Response<PaginaModelo>> BCreate(PaginaRequest createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<bool>> BUpdate(PaginaRequest updateModel);
    }
}
