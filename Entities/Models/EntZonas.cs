using Newtonsoft.Json;
namespace Entities.Models
{
    public class EntZonas
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("IdIglesia")]
        public Guid uIdIglesia { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime? dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime? dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
