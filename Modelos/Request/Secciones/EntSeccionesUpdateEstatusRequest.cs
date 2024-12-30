using Newtonsoft.Json;

namespace Modelos.Request.Secciones
{
    public class EntSeccionesUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }

    }
}
