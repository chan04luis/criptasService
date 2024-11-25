using Newtonsoft.Json;

namespace Entities.Request.Beneficiarios
{
    public class EntBeneficiariosRequest
    {
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
    }
}
