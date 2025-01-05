using Models.Request.Seguridad;
using Models.Seguridad;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusPerfiles
    {
        Task<Response<PerfilModelo>> BCreate(PerfilRequest createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<PerfilModelo>> BGet(Guid iKey);
        Task<Response<List<PerfilModelo>>> BGetAll();
        Task<Response<bool>> BUpdate(PerfilRequest updateModel);
    }
}
