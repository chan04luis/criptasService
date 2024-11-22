using Newtonsoft.Json;

namespace Entities.JsonRequest.Iglesias
{
    public class EntIglesiaRequest
    {
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("Ciudad")]
        public string sCiudad { get; set; }
    }
}
