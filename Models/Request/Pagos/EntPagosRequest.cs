using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Pagos
{
    public class EntPagosRequest
    {
        [JsonProperty("IdCliente")]
        public Guid uIdClientes { get; set; }

        [JsonProperty("IdCripta")]
        public Guid uIdCripta { get; set; }

        [JsonProperty("IdTipoPago")]
        public Guid uIdTipoPago { get; set; }

        [JsonProperty("MontoTotal")]
        public decimal dMontoTotal { get; set; }

        [JsonProperty("FechaLimite")]
        public DateTime dtFechaLimite { get; set; }
    }
}
