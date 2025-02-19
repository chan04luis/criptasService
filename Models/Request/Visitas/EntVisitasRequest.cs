using Newtonsoft.Json;

namespace Models.Request.Visitas
{
    public class EntVisitasRequest
    {
        [JsonProperty("NombreVisitante")]
        public string? sNombreVisitante { get; set; }
        [JsonProperty("Mensaje")]
        public string? sMensaje { get; set; }

        [JsonProperty("IdCriptas")]
        public Guid uIdCriptas { get; set; }
    }
}
