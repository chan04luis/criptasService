using Newtonsoft.Json;

namespace Models.Request.Iglesias
{
    public class EntIglesiaMaps
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Latitud")]
        public string? sLatitud { get; set; }
        [JsonProperty("Longitud")]
        public string? sLongitud { get; set; }
    }
}
