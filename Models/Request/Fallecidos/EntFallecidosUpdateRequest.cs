using Newtonsoft.Json;

namespace Models.Request.Fallecidos
{
    public class EntFallecidosUpdateRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Nacimiento")]
        public string dtFechaNacimiento { get; set; }

        [JsonProperty("Fallecimiento")]
        public string dtFechaFallecimiento { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }
    }
}
