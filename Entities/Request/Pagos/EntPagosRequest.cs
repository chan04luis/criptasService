using Newtonsoft.Json;

namespace Entities.Request.Pagos
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
