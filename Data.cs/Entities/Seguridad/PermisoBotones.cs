using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class PermisoBotones
    {
        public Guid uIdPermisoBoton { get; set; }
        public Guid uIdPerfil { get; set; }
        public Guid uIdBoton { get; set; }
        public bool? bTienePermiso { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public DateTime dtFechaModificacion { get; set; }
        public Guid? uIdUsuarioModificacion { get; set; }
        public bool? bActivo { get; set; }
    }
}
