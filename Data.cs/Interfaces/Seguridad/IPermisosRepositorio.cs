using Models.Seguridad;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IPermisosRepositorio
    {
        Task<Response<List<PermisoElementoSrcModelo>>> GetPermisosElementos(Guid piIdPerfil);
        Task<Response<bool>> GuardarPermisosModulos(Guid uIdModulo, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<bool>> ActualizarPermisosModulos(Guid idPermisoModulo, Guid uIdModulo, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<bool>> GuardarPermisoPaginas(Guid uIdPagina, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<bool>> ActualizarPermisosPaginas(Guid idPermisoPagina, Guid uIdPagina, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<bool>> GuardarPermisoBotones(Guid uIdBoton, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<bool>> ActualizarPermisosBotones(Guid idPermisoBoton, Guid uIdBoton, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<List<PerfilPermisoMenuModelo>>> GetPermisosMenu(Guid piIdPerfil);
    }
}
