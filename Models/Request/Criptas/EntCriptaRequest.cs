using Newtonsoft.Json;

namespace Models.Request.Criptas
{
    public class EntCriptaRequest
    {
        [JsonProperty("IdSeccion")]
        public Guid uIdSeccion { get; set; }

        [JsonProperty("IdCliente")]
        public Guid uIdCliente { get; set; }

        [JsonProperty("Numero")]
        public string sNumero { get; set; }

        [JsonProperty("Precio")]
        public decimal dPrecio { get; set; }

        [JsonProperty("UbicacionEspecifica")]
        public string sUbicacionEspecifica { get; set; }
    }
}
