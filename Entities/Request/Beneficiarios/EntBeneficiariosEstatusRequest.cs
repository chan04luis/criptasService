using Newtonsoft.Json;

namespace Entities.Request.Beneficiarios
{
    public class EntBeneficiariosRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("IdCripta")]
        public Guid uIdCripta { get; set; }
    }

    public class BeneficiariosEstatusRequest
    {
        [JsonProperty("FechaRegistrio")]
        public DateTime dtFechaRegistro { get; set; }
        public DateTime dtFechaActualizacion { get; set; }
        public DateTime dtFechaEliminado { get; set; }
        public bool bEstatus { get; set; }
        public bool bEliminado { get; set; }
    }

    public class EntBeneficiarioDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
}
