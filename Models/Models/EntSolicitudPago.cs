using Newtonsoft.Json;

namespace Models.Models
{
    public class EntSolicitudPago
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
        [JsonProperty("IdPago")]
        public Guid uIdPago { get; set; }
        [JsonProperty("Evidencia")]
        public string? sEvidencia { get; set; }
        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }
        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActualizacion { get; set; }
    }
}
