using Models.Request.Seguridad;
using Models.Seguridad;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusModulo
    {
        Task<Response<ModuloModelo>> BCreate(ModuloRequest createModel);
        Task<Response<bool>> BDelete(Guid iKey);
        Task<Response<ModuloModelo>> BGet(Guid iKey);
        Task<Response<List<ModuloModelo>>> BGetAll();
        Task<Response<bool>> BUpdate(ModuloRequest updateModel);
    }
}
