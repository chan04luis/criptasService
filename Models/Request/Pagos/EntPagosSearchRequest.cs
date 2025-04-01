
using Newtonsoft.Json;

namespace Models.Request.Pagos
{
    public class EntPagosSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }

        [JsonProperty("Cliente")]
        public string? sCliente { get; set; }

        [JsonProperty("Cripta")]
        public Guid? uIdCripta { get; set; }
        [JsonProperty("Seccion")]
        public Guid? uIdSeccion { get; set; }
        [JsonProperty("Zona")]
        public Guid? uIdZona { get; set; }
        [JsonProperty("Iglesia")]
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
