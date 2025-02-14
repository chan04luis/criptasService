using Newtonsoft.Json;

namespace Models.Request.Fallecidos
{
    public class EntFallecidosUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
