using Newtonsoft.Json;

namespace Models.Responses.Pagos
{
    public class EntCriptasLista
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

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

        [JsonProperty("IdCliente")]
        public Guid uIdCliente { get; set; }

        [JsonProperty("NombreCliente")]
        public string sNombreCliente { get; set; }

        [JsonProperty("ApellidosCliente")]
        public string sApellidosCliente { get; set; }

        [JsonProperty("Numero")]
        public string sNumero { get; set; }

        [JsonProperty("Precio")]
        public decimal dPrecio { get; set; }

        [JsonProperty("UbicacionEspecifica")]
        public string sUbicacionEspecifica { get; set; }

        [JsonProperty("FechaRegistro")]
        public DateTime dtFechaRegistro { get; set; }

        [JsonProperty("FechaActualizacion")]
        public DateTime dtFechaActualizacion { get; set; }

        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }

        [JsonProperty("Disponible")]
        public bool bDisponible { get; set; }
    }
}
