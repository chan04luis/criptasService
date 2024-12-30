using Newtonsoft.Json;

namespace Modelos.Request.Pagos
{
    public class ReadPagosRequest
    {
        [JsonProperty("IdPago")]
        public Guid uIdPago { get; set; }

        [JsonProperty("MontoPagado")]
        public decimal dMontoPagado { get; set; }
    }
}
