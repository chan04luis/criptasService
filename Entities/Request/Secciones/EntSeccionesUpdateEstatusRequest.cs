using Newtonsoft.Json;

namespace Entities.Request.Secciones
{
    public class EntSeccionesUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }

    }
}
