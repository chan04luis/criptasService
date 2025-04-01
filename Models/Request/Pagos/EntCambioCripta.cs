
using Newtonsoft.Json;

namespace Models.Request.Pagos
{
    public class EntCambioCripta
    {
        [JsonProperty("idPago")]
        public Guid uId { get; set; }

        [JsonProperty("idCripta")]
        public Guid? uIdCripta { get; set; }

        [JsonProperty("idCriptaNuevo")]
        public Guid uIdCriptaNuevo { get; set; }
    }
}
