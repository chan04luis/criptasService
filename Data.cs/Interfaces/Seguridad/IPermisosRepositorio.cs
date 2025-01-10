using Data.cs.Entities.Seguridad;
using Models.Seguridad;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IPermisosRepositorio
    {
        Task<Response<List<Modulo>>> GetPermisosElementos(Guid piIdPerfil);
        Task<Response<bool>> GuardarPermisosModulos(IEnumerable<PermisoModuloModelo> permisosModulosModelo, Guid idPerfil);
        Task<Response<bool>> ActualizarPermisosModulos(IEnumerable<PermisoModuloModelo> permisosModulosModelo, Guid idPerfil);
        Task<Response<bool>> GuardarPermisoPaginas(IEnumerable<PermisoPaginaModelo> permisosPaginaModelo, Guid idPerfil);
        Task<Response<bool>> ActualizarPermisosPaginas(IEnumerable<PermisoPaginaModelo> permisosPaginasModelo, Guid idPerfil);
        Task<Response<bool>> GuardarPermisoBotones(IEnumerable<PermisoBotonModelo> permisosBotonModelo, Guid idPerfil);
        Task<Response<bool>> ActualizarPermisosBotones(IEnumerable<PermisoBotonModelo> permisosBotonesModelo, Guid idPerfil);
        Task<Response<List<PerfilPermisoMenuModelo>>> GetPermisosMenu(Guid piIdPerfil);

    }
}
