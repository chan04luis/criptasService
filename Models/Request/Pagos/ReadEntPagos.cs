using Newtonsoft.Json;

namespace Models.Request.Pagos
{
    public class ReadPagosRequest
    {
        [JsonProperty("IdPago")]
        public Guid uIdPago { get; set; }

        [JsonProperty("MontoPagado")]
        public decimal dMontoPagado { get; set; }

        [JsonProperty("ApplyDate")]
        public bool bApplyDate { get; set; }

        [JsonProperty("FechaPagado")]
        public string sFechaPagado { get; set; }
    }
}
