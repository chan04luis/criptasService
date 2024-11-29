using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntPagos
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

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

        [JsonProperty("Pagado")]
        public bool bPagado { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }

        [JsonProperty("MontoPagado")]
        public decimal dMontoPagado { get; set; }

        [JsonProperty("FechaPagado")]
        public DateTime dtFechaPagado { get; set; }
    }
}
