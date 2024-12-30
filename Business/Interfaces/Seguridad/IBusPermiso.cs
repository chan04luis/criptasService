using Microsoft.AspNetCore.Http;
using Modelos.Seguridad;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusPermiso
    {
        Task<Response<PerfilPermisosModelo>> ObtenerPermisos(Guid idPerfil);
        Task<Response<bool>> GuardarPermisos(IFormCollection form);
        Task<Response<object>> ObtenerPermisosMenu(Guid idPerfil);
    }
}
