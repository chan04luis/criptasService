using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntFallecidos
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("IdCrita")]
        public string uIdCripta { get; set; }
        [JsonProperty("sNombre")]
        public string sNombre { get; set; }
        [JsonProperty("FechaFallecimiento")]
        public DateTime dtFechaFallecimiento { get; set; }
        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }
        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActializacion { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}