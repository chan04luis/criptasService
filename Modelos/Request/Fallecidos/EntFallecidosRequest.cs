
using Newtonsoft.Json;

namespace Modelos.Request.Fallecidos
{
    public class EntFallecidosRequest
    {

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }
    }
}
