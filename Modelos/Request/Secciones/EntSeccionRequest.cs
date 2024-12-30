using Newtonsoft.Json;

namespace Modelos.Request.Secciones
{
    public class EntSeccionRequest
    {
        [JsonProperty("IdZona")]
        public Guid uIdZona { get; set; }
        [JsonProperty("Nombre")]
        public string sNombre { get; set; } 
    }

}
