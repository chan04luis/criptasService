using Newtonsoft.Json;

namespace Models.Request.Catalogo.Sucursales
{
    public class EntSucursalMaps
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Latitud")]
        public string? sLatitud { get; set; }
        [JsonProperty("Longitud")]
        public string? sLongitud { get; set; }
    }
}
