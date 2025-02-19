using Newtonsoft.Json;

namespace Models.Models
{
    public class EntVisitas
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("NombreVisitante")]
        public string? sNombreVisitante { get; set; }

        [JsonProperty("NombreFallecido")]
        public string? sNombreFallecido { get; set; }
        [JsonProperty("Mensaje")]
        public string? sMensaje { get; set; }

        [JsonProperty("IdCriptas")]
        public Guid uIdCriptas { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime? dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}