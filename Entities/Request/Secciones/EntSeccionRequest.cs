using Newtonsoft.Json;

namespace Entities.Request.Secciones
{
    public class EntSeccionRequest
    {
        [JsonProperty("IdZona")]
        public Guid uIdZona { get; set; }
        [JsonProperty("Nombre")]
        public string sNombre { get; set; } 
    }

}
