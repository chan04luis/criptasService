using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class PermisoModuloModelo
    {
        public Guid idPermisoModulo { get; set; }
        public Guid idModulo { get; set; }
        public string? claveModulo { get; set; }
        public string? nombreModulo { get; set; }
        public bool? tienePermiso { get; set; }
        public List<PermisoPaginaModelo>? permisosPagina { get; set; }
    }
}
