using Newtonsoft.Json;

namespace Entities.Request.Secciones
{
    public class EntSeccionSearchRequest
    {
        [JsonProperty("IdZona")]
        public Guid? uIdZona { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
