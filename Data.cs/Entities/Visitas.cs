using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class Visitas
    {
        public Guid uId { get; set; }
        public string sNombreVisitante { get; set; }
        public Guid uIdCriptas { get; set; }
        public DateTime dtFechaVisita {  get; set; }
        public DateTime dtFechaResgistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool bEstatus {  get; set; }
        public bool bEliminado { get; set; }
    }
}
