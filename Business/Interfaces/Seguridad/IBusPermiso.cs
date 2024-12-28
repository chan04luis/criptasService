using Entities;
using Microsoft.AspNetCore.Http;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Seguridad
{
    public interface IBusPermiso
    {
        Task<Response<PerfilPermisosModelo>> ObtenerPermisos(Guid idPerfil);
        Task<Response<bool>> GuardarPermisos(IFormCollection form);
        Task<Response<object>> ObtenerPermisosMenu(Guid idPerfil);
    }
}
