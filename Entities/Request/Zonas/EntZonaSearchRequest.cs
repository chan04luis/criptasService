using Newtonsoft.Json;
namespace Entities.JsonRequest.Zonas
{
    public class EntZonaSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }

        [JsonProperty("Nombre")]
        public string? sNombre { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
