using Data.cs.Entities.Seguridad;
using Models.Seguridad;
using Utils;

namespace Data.cs.Interfaces.Seguridad
{
    public interface IPermisosRepositorio
    {
        Task<Response<List<Modulo>>> GetPermisosElementos(Guid piIdPerfil);
        Task<Response<bool>> GuardarPermisosModulos(Guid uIdModulo, Guid idPerfil, bool btienePermiso, Guid idUsuario);
        Task<Response<bool>> ActualizarPermisosModulos(Guid idPermisoModulo, Guid uIdModulo, Guid idPerfil, bool btienePermiso, Guid idUsuario);
        Task<Response<bool>> GuardarPermisoPaginas(Guid uIdPagina, Guid idPerfil, bool btienePermiso, Guid idUsuario);
        Task<Response<bool>> ActualizarPermisosPaginas(Guid idPermisoPagina, Guid uIdPagina, Guid idPerfil, bool btienePermiso, Guid idUsuario);
        Task<Response<bool>> GuardarPermisoBotones(Guid uIdBoton, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<bool>> ActualizarPermisosBotones(Guid idPermisoBoton, Guid uIdBoton, Guid idPerfil, bool? btienePermiso, Guid idUsuario);
        Task<Response<List<PerfilPermisoMenuModelo>>> GetPermisosMenu(Guid piIdPerfil);
        /*Task<Response<List<PermisoModulos>>> GetPermisosModulos(Guid piIdPerfil);
        Task<Response<List<PermisosPagina>>> GetPermisosPaginas(Guid piIdPerfil);
        Task<Response<List<PermisoBotones>>> GetPermisosBotones(Guid piIdPerfil);*/
    }
}
