using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class PermisoElementoSrcModelo:ElementoSrcModelo
    {
        public Guid idPermisoModulo { get; set; }
        public Guid idPermisoPagina { get; set; }
        public Guid idPermisoBoton { get; set; }
        public bool? TienePermisoModulo { get; set; }
        public bool? TienePermisoPagina { get; set; }
        public bool? TienePermisoBoton { get; set; }
    }
}
