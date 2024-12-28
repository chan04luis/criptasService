
using Entities;
using Entities.JsonRequest.Usuarios;
using Entities.Request.Usuarios;

namespace Business.Data
{
    public interface IUsuariosRepositorio
    {
        Task<Response<bool>> AnyExistKey(Guid pKey);
        Task<Response<bool>> AnyExitMailAndKey(EntUsuarios pEntity);
        Task<Response<bool>> DAnyExistEmail(string pCorreo);
        Task<Response<EntUsuarios>> DSave(EntUsuarios entity);
        Task<Response<EntUsuarios>> DUpdate(EntUsuarios entity);
        Task<Response<EntUsuarios>> DUpdateEstatus(EntUsuarios entity);
        Task<Response<bool>> DDelete(Guid uId);
        Task<Response<EntUsuarios>> DGetById(Guid iKey);
        Task<Response<List<EntUsuarios>>> DGetList();
        Task<Response<List<EntUsuarios>>> DGetByFilters(EntUsuarioSearchRequest search);
        Task<Response<EntUsuarios>> DLogin(EntUsuarioLoginRequest loginRequest);
    }
}
