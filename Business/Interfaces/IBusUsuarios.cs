using Entities.JsonRequest.Usuarios;
using Entities.Request.Usuarios;
using Entities;
using Entities.Responses.Usuarios;

namespace Business.Interfaces
{
    public interface IBusUsuarios
    {
        Task<Response<EntUsuarios>> ValidateAndSaveUser(EntUsuarioRequest usuario);

        Task<Response<EntUsuarios>> ValidateAndUpdateUser(EntUsuarioUpdateRequest usuario);

        Task<Response<EntUsuarios>> UpdateUserStatus(EntUsuarioUpdateEstatusRequest usuario);

        Task<Response<bool>> DeleteUserById(Guid id);

        Task<Response<EntUsuarios>> GetUserById(Guid id);

        Task<Response<List<EntUsuarios>>> GetUsersByFilters(EntUsuarioSearchRequest filtros);

        Task<Response<List<EntUsuarios>>> GetUserList();
        Task<Response<AuthLogin>> getLogin(EntUsuarioLoginRequest usuario);
    }
}
