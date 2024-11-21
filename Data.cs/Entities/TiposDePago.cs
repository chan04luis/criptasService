using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class TiposDePago
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string descripcaon { get; set; }
        public DateTime? fecha_registro { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
        public bool? estatus {  get; set; }
    }
}
