using Newtonsoft.Json;

namespace Modelos.Request.Fallecidos
{
    public class EntFallecidosUpdateRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }
    }
}
