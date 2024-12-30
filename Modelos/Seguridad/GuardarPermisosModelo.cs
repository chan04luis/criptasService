using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class GuardarPermisosModelo
    {
        public Guid idPerfil { get; set; }
        public List<PermisoModuloModelo>? permisos { get; set; }
    }
}
