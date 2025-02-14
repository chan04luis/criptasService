using Newtonsoft.Json;

namespace Models.Request.Fallecidos
{
    public class EntFallecidosRequest
    {

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Nacimiento")]
        public DateTime dNacimiento { get; set; }

        [JsonProperty("Fallecimiento")]
        public DateTime dFallecimiento { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }
    }
}
