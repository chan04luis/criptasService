
using Newtonsoft.Json;

namespace Entities.Request.Pagos
{
    public class EntPagosSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }

        [JsonProperty("IdCliente")]
        public Guid? uIdClientes { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }

        [JsonProperty("IdTipoPago")]
        public Guid? uIdTipoPago { get; set; }

        [JsonProperty("Pagado")]
        public bool? bPagado { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
