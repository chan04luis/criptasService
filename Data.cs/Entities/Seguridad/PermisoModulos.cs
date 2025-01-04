using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class PermisoModulos
    {
        public Guid uIdPermisoModulo { get; set; }
        public Guid uIdPerfil { get; set; }
        public Guid uIdModulo { get; set; }
        public bool? bTienePermiso { get; set; }
        public DateTime dtFechaCreacion { get; set; }
        public DateTime dtFechaModificacion { get; set; }
        [NotMapped]
        public Guid? uIdUsuarioModificacion { get; set; }
        public bool? bActivo { get; set; }
    }
}
