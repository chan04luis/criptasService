using Utils;
using Modelos.Models;
using Modelos.Request.Usuarios;
using Modelos.Responses.Iglesia;

namespace Business.Interfaces.Seguridad
{
    public interface IBusUsuarios
    {
        Task<Response<EntUsuarios>> SaveUser(EntUsuarioRequest usuario);

        Task<Response<EntUsuarios>> UpdateUser(EntUsuarioUpdateRequest usuario);

        Task<Response<EntUsuarios>> UpdateUserStatus(EntUsuarioUpdateEstatusRequest usuario);

        Task<Response<bool>> DeleteUserById(Guid id);

        Task<Response<EntUsuarios>> GetUserById(Guid id);

        Task<Response<List<EntUsuarios>>> GetUsersByFilters(EntUsuarioSearchRequest filtros);

        Task<Response<List<EntUsuarios>>> GetUserList();
        Task<Response<AuthLogin>> getLogin(EntUsuarioLoginRequest usuario);
    }
}
