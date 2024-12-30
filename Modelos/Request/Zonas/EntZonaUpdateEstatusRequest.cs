using Newtonsoft.Json;
namespace Modelos.Request.Zonas
{
    public class EntZonaUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
