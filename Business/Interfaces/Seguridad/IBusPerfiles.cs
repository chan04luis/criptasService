using Modelos.Seguridad;
using Utils;

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
