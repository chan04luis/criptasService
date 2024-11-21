using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public class Pagos
    {
        public Guid id { get; set; }
        public Guid id_clientes { get; set; }
        public Guid id_cripta {  get; set; }
        public Guid id_tipo_pago { get; set; }
        public Decimal monto_total { get; set; }
        public DateTime? fecha_limite { get; set; }
        public bool? pagado {  get; set; }
        public DateTime? fecha_registro { get; set; }
        public DateTime? fecha_actualizacion { get; set; }
        public bool? estatus { get; set; }

    }
}
