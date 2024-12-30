using Newtonsoft.Json;

namespace Modelos.Request.Criptas
{
    public class EntCriptaUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
