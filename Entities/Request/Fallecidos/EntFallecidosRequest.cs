
using Newtonsoft.Json;

namespace Entities.Request.Fallecidos
{
    public class EntFallecidosRequest
    {

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }
    }
}
