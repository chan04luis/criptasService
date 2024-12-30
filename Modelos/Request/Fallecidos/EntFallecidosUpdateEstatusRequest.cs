
using Newtonsoft.Json;

namespace Modelos.Request.Fallecidos
{
    public class EntFallecidosUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
