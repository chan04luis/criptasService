using Newtonsoft.Json;

namespace Models.Models
{
    public class EntFallecidos
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Edad")]
        public int iEdad { get; set; }

        [JsonProperty("Nacimiento")]
        public string? dtFechaNacimiento { get; set; }

        [JsonProperty("Fallecimiento")]
        public string? dtFechaFallecimiento { get; set; }

        [JsonProperty("ActaDefuncion")]

        public string? sActaDefuncion { get; set; }

        [JsonProperty("AutorizacionIncineracion")]
        public string? sAutorizacionIncineracion { get; set; }

        [JsonProperty("AutorizacionTraslado")]
        public string? sAutorizacionTraslado { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }

        [JsonProperty("IdCripta")]
        public Guid uIdCripta { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActualizacion { get; set; }
    }
}