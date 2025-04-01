using Newtonsoft.Json;

namespace Models.Request.Visitas
{
    public class EntVisitasSearchRequest
    {
        [JsonProperty("NombreVisitante")]
        public string? sNombreVisitante { get; set; }

        [JsonProperty("IdCriptas")]
        public Guid? uIdCriptas { get; set; }
        [JsonProperty("FechaInicio")]
        public DateTime? dtFechaInicio { get; set; }

        [JsonProperty("FechaFin")]
        public DateTime? dtFechaFin { get; set; }
    }
}
