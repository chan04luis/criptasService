using Newtonsoft.Json;

namespace Entities.JsonRequest.Zonas
{
    public class EntZonaRequest
    {
        [JsonProperty("IdIglesia")]
        public Guid uIdIglesia { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
