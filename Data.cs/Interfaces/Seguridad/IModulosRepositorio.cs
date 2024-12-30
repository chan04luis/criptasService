using Data.cs.Entities.Seguridad;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IModulosRepositorio
    {
        Task<Response<Modulo>> DSave(Modulo newItem);
        Task<Response<bool>> DDelete(Guid iKey);
        Task<Response<List<Modulo>>> DGet();
        Task<Response<Modulo>> DGet(Guid iKey);
        Task<Response<bool>> DUpdate(Modulo entity);
    }
}
