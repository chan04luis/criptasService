using Data.cs.Entities.Seguridad;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IBotonesRepositorio
    {
        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExitNameAndKey(Boton pEntity);
        Task<Response<bool>> AnyExitName(string pName);
        Task<Response<bool>> Delete(Guid iKey);
        Task<Response<Boton>> Save(Boton newItem);
        Task<Response<bool>> Update(Boton entity);
    }
}
