using Newtonsoft.Json;

namespace Modelos.Request.Fallecidos
{
    public class EntFallecidosSearchRequest
    {
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
