using Entities.Models;
using Newtonsoft.Json;

namespace Entities.Responses.Zonas
{
    public class EntZonasResponse
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
        [JsonProperty("Secciones")]
        public List<EntSecciones>? Items { get; set; }
    }
}
