using Newtonsoft.Json;

namespace Modelos.Request.TipoPagos
{
    public class EntTiposPagoUpdateEstatusRequest
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
    }
}
