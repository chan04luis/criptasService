using Newtonsoft.Json;

namespace Models.Responses.Criptas
{
    public class CriptasDisponibles
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Cripta")]
        public string sNombre { get; set; }
        [JsonProperty("Precio")]
        public decimal dPrecio { get; set; }
        [JsonProperty("Seccion")]
        public string sNombreSeccion { get; set; }
        [JsonProperty("Zona")]
        public string sNombreZona { get; set; }
        [JsonProperty("Iglesia")]
        public string sIglesia { get; set; }
        [JsonProperty("Lat")]
        public string sLatitud { get; set; }
        [JsonProperty("Long")]
        public string sLongitud { get; set; }
        [JsonProperty("Estatus")]
        public bool bEstatus { get; set; }
        [JsonProperty("Disponible")]
        public bool bDisponible { get; set; }
    }
}
