using Models.Models;
using Models.Request.Usuarios;
using Utils;
using Utils.Interfaces;

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
        Task<Response<EntUsuarios>> DGet(string correo, string sPassword);
        Task<Response<EntUsuarios>> DGetByIdAndPerfilAsync(Guid usuarioId, Guid perfilId);
    }
}
