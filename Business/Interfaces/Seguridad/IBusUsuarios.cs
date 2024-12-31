using Utils;
using Models.Models;
using Models.Request.Usuarios;
using Models.Responses.Iglesia;
using Models.Responses.Usuarios;

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
