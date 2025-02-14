using Newtonsoft.Json;

namespace Models.Models
{
    public class EntBeneficiarios
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

        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
