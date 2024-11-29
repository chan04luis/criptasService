using Newtonsoft.Json;

namespace Entities.Request.Fallecidos
{
    public class EntFallecidosSearchRequest
    {
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
