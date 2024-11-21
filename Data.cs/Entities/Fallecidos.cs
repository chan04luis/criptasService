using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class Fallecidos
    {
        public Guid id { get; set; }
        public Guid id_cirpta { get; set; }
        public string nombre { get; set; }
        public DateTime? fecha_fallecimiento { get; set; }
        public DateTime? fecha_registro { get; set; }
        public DateTime? fecha_actializacion { get; set; }
        public bool? estatus {  get; set; }  
    }
}
