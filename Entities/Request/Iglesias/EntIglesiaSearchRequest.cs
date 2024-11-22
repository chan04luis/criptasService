using Newtonsoft.Json;

namespace Entities.JsonRequest.Iglesias
{
    public class EntIglesiaSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }
        [JsonProperty("Nombre")]
        public string? sNombre { get; set; }
        [JsonProperty("Ciudad")]
        public string? sCiudad { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
