using Data.cs.Entities.Seguridad;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IPaginasRespositorio
    {
        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExitNameAndKey(Pagina pEntity);
        Task<Response<bool>> AnyExitName(string pName);
        Task<Response<bool>> Delete(Guid iKey);
        Task<Response<List<Pagina>>> GetAll();
        Task<Response<Pagina>> Get(Guid iKey);
        Task<Response<Pagina>> Save(Pagina newItem);
        Task<Response<bool>> Update(Pagina entity);
    }
}
