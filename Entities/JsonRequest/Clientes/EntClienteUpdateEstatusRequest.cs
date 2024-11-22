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
}
