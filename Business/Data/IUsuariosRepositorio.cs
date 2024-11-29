
using Entities;
using Entities.Request.Usuarios;

namespace Business.Data
{
    public interface IUsuariosRepositorio
    {
        Task<Response<EntUsuarios>> DSave(EntUsuarios entity);
        Task<Response<EntUsuarios>> DUpdate(EntUsuarios entity);
        Task<Response<EntUsuarios>> DUpdateBoolean(EntUsuarios entity);
        Task<Response<bool>> DUpdateEliminado(Guid uId);
        Task<Response<EntUsuarios>> DGetById(Guid iKey);
        Task<Response<EntUsuarios>> DGetByEmail(string correo);
        Task<Response<List<EntUsuarios>>> DGetList();
        Task<Response<List<EntUsuarios>>> DGetByFilters(EntUsuarioSearchRequest search);
        Task<Response<EntUsuarios>> DLogin(EntUsuarioLoginRequest loginRequest);
    }
}
