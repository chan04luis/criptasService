using Newtonsoft.Json;

namespace Modelos.Request.Iglesias
{
    public class EntIglesiaUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
