using Newtonsoft.Json;

namespace Entities.JsonRequest.Iglesias
{
    public class EntIglesiaDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
}
