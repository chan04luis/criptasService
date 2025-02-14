using Newtonsoft.Json;

namespace Models.Request.Beneficiarios
{
    public class EntBeneficiariosUpdateRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("IdCripta")]
        public Guid uIdCripta { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("IneFrente")]
        public string? sIneFrente { get; set; }

        [JsonProperty("IneReverso")]
        public string? sIneReverso { get; set; }
    }
}
