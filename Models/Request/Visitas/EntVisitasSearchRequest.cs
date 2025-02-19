using Newtonsoft.Json;

namespace Models.Request.Visitas
{
    public class EntVisitasSearchRequest
    {
        [JsonProperty("NombreVisitante")]
        public string? sNombreVisitante { get; set; }

        [JsonProperty("IdCriptas")]
        public Guid? uIdCriptas { get; set; }
    }
}
