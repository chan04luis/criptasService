using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class Modulo
    {
        public Guid uIdModulo { get; set; }
        public string? sClaveModulo { get; set; }
        public string? sNombreModulo { get; set; }
        public string? sPathModulo { get; set; }
        public bool bMostrarEnMenu { get; set; }
        public DateTime? dtFechaCreacion { get; set; }
        public Guid? uIdUsuarioCreacion { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public Guid? uIdUsuarioModificacion { get; set; }
        public DateTime? dtFechaEliminacion { get; set; }
        public Guid? uIdUsuarioEliminacion { get; set; }
        public bool? bActivo { get; set; }
        public bool? bBaja { get; set; }
    }
}
