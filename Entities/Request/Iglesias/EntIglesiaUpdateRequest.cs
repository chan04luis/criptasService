using Newtonsoft.Json;
namespace Entities.JsonRequest.Iglesias
{
    public class EntIglesiaUpdateRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("Ciudad")]
        public string sCiudad { get; set; }
    }
}
