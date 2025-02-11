
using Newtonsoft.Json;

namespace Models.Request.Pagos
{
    public class EntPagosSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }

        [JsonProperty("Cliente")]
        public string? sCliente { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }
        [JsonProperty("IdSeccion")]
        public Guid? uIdSeccion { get; set; }
        [JsonProperty("IdZona")]
        public Guid? uIdZona { get; set; }
        [JsonProperty("IdIglesia")]
        public Guid? uIdIglesia { get; set; }

        [JsonProperty("IdTipoPago")]
        public Guid? uIdTipoPago { get; set; }

        [JsonProperty("Pagado")]
        public bool? bPagado { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
        [JsonProperty("NumPag")]
        public int iNumPag { get; set; }
        [JsonProperty("NumReg")]
        public int iNumReg { get; set; }
    }
}
