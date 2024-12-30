
using Newtonsoft.Json;

namespace Modelos.Request.Zonas
{
    public class EntZonaDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
}
