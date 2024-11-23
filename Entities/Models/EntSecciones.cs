using Newtonsoft.Json;

namespace Entities.Models
{
    public class EntSecciones
    {
        [JsonProperty("id")]
        public Guid uId { get; set; }

        [JsonProperty("idZona")]
        public Guid uIdZona { get; set; }

        [JsonProperty("nombre")]
        public string sNombre { get; set; }

        [JsonProperty("fechaRegistro")]
        public DateTime? dtFechaRegistro { get; set; }

        [JsonProperty("fechaActualizacion")]
        public DateTime? dtFechaActualizacion { get; set; }

        [JsonProperty("estatus")]
        public bool bEstatus { get; set; }
    }
}
