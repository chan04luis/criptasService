using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntPagosParciales
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("IdPago")]
        public Guid uIdPago { get; set; }

        [JsonProperty("Monto")]
        public decimal dMonto { get; set; }

        [JsonProperty("FechaPago")]
        public DateTime dtFechaPago { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }

}
