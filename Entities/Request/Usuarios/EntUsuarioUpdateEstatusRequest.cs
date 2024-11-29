using Newtonsoft.Json;

namespace Entities.Request.Usuarios
{
    public class EntUsuarioUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
