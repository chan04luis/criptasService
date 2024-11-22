using Newtonsoft.Json;

namespace Entities.JsonRequest.Clientes
{
    public class EntClienteUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
    public class EntClienteDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
    public class EntClienteSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }
        [JsonProperty("Nombre")]
        public string? sNombre { get; set; }
        [JsonProperty("Apellido")]
        public string? sApellido { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
