using Microsoft.AspNetCore.Http;
using Models.Seguridad;
using Utils;

namespace Business.Interfaces.Seguridad
{
    public interface IBusPermiso
    {
        Task<Response<PerfilPermisosModelo>> ObtenerPermisos(Guid idPerfil);
        Task<Response<bool>> GuardarPermisos(GuardarPermisosModelo lstPermisosElementos);
        Task<Response<object>> ObtenerPermisosMenu(Guid idPerfil);
    }
}
