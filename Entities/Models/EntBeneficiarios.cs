using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntBeneficiarios
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("IdCriptas")]
        public Guid uIdCripta { get; set; }
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
        [JsonProperty("FechaFallecimiento")]
        public DateTime dtFachaFallecimiento { get; set; }
        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }
        [JsonProperty("FechaActulizacion")]
        public DateTime dtFechaActualizacion { get; set; }
        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
