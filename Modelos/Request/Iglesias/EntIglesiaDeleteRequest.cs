using Newtonsoft.Json;

namespace Modelos.Request.Iglesias
{
    public class EntIglesiaDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
}
