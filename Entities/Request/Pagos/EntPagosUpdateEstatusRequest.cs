using Newtonsoft.Json;

namespace Entities.Request.Pagos
{
    public class EntPagosUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
