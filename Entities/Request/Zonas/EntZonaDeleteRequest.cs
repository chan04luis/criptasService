
using Newtonsoft.Json;

namespace Entities.JsonRequest.Zonas
{
    public class EntZonaDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
}
