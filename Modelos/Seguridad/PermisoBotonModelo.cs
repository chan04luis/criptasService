using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class PermisoBotonModelo
    {
        public Guid idPermisoBoton { get; set; }
        public Guid idBoton { get; set; }
        public string? claveBoton { get; set; }
        public string? nombreBoton { get; set; }
        public bool? tienePermiso { get; set; }
    }
}
