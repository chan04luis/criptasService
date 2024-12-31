using Newtonsoft.Json;

namespace Models.Request.Pagos
{
    public class EntPagosUpdatePagadoRequest
    {
        [JsonProperty("IdPago")]
        public Guid uIdPago { get; set; }

        [JsonProperty("MontoPagado")]
        public decimal? dMontoPagado { get; set; }

        [JsonProperty("FechaPagado")]
        public DateTime? dtFechaPagado { get; set; }

        [JsonProperty("Pagado")]
        public bool bPagado { get; set; }
    }
}
