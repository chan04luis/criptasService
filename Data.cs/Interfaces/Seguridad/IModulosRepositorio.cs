using Data.cs.Entities.Seguridad;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IModulosRepositorio
    {
        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExitNameAndKey(Modulo pEntity);
        Task<Response<bool>> AnyExitName(string pName);
        Task<Response<bool>> Delete(Guid iKey);
        Task<Response<List<Modulo>>> GetAll();
        Task<Response<Modulo>> Get(Guid iKey);
        Task<Response<Modulo>> Save(Modulo newItem);
        Task<Response<bool>> Update(Modulo entity);
    }
}
