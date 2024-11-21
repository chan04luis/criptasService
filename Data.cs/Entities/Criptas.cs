using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class Criptas
    {
        public  Guid id { get; set; } 
        public Guid id_seccion { get; set; }
        public Guid id_clientes { get; set; }
        public string numero { get; set; }
        public string ubicacion_especifica { get; set; }
        public DateTime? fecha_registro { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
        public bool? estatus {  get; set; }
    }
}
