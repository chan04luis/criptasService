using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class Visitas
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public Guid id_criptas { get; set; }
        public DateTime fecha_visita {  get; set; }
        public DateTime fecha_resgistro { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public bool estatus {  get; set; }
    }
}
