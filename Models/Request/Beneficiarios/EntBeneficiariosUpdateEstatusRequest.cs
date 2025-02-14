using Newtonsoft.Json;

namespace Models.Request.Beneficiarios
{
    public class EntBeneficiariosUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
