using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PermisoPaginaModelo
    {
        public Guid idPermisoPagina { get; set; }
        public Guid idPagina { get; set; }
        public string? clavePagina { get; set; }
        public string? nombrePagina { get; set; }
        public bool tienePermiso { get; set; }
        public List<PermisoBotonModelo>? permisosBoton { get; set; }
    }
}
