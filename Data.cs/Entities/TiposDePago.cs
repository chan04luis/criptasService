using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class TiposDePago
    {
        public Guid uId { get; set; }
        public string sNombre { get; set; }
        public string sDescripcion { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public bool? bEstatus {  get; set; }
    }
}
