using Newtonsoft.Json;

namespace Models.Responses.Pagos
{
    public class EntPagosLista
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("IdCliente")]
        public Guid uIdClientes { get; set; }

        [JsonProperty("ClavePago")]
        public string sClavePago { get; set; }

        [JsonProperty("NombreCliente")]
        public string sNombreCliente { get; set; }

        [JsonProperty("ApellidosCliente")]
        public string sApellidosCliente { get; set; }

        [JsonProperty("IdCripta")]
        public Guid uIdCripta { get; set; }

        [JsonProperty("NumeroCripta")]
        public string sNumeroCripta { get; set; }

        [JsonProperty("IdSeccion")]
        public Guid uIdSeccion { get; set; }

        [JsonProperty("NombreSeccion")]
        public string sNombreSeccion { get; set; }

        [JsonProperty("IdZona")]
        public Guid uIdZona { get; set; }

        [JsonProperty("NombreZona")]
        public string sNombreZona { get; set; }

        [JsonProperty("IdIglesia")]
        public Guid uIdIglesia { get; set; }

        [JsonProperty("NombreIglesia")]
        public string sNombreIglesia { get; set; }

        [JsonProperty("IdTipoPago")]
        public Guid uIdTipoPago { get; set; }

        [JsonProperty("MontoTotal")]
        public decimal dMontoTotal { get; set; }

        [JsonProperty("MontoPagado")]
        public decimal? dMontoPagado { get; set; }

        [JsonProperty("FechaLimite")]
        public DateTime dtFechaLimite { get; set; }

        [JsonProperty("FechaPagado")]
        public DateTime? dtFechaPagado { get; set; }

        [JsonProperty("Pagado")]
        public bool bPagado { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
