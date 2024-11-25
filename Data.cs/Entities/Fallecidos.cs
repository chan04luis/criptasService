using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class Fallecidos
    {
        public Guid uId { get; set; }
        public Guid uIdCripta { get; set; }
        public string sNombre { get; set; }
        public DateTime dtFechaFallecimiento { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActializacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool? bEstatus {  get; set; }
        public bool bEliminado { get; set; }
    }
}
