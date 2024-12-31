using Newtonsoft.Json;

namespace Models.Request.Pagos
{
    public class EntPagosUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
