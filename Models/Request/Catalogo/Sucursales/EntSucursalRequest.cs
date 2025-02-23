using Newtonsoft.Json;

namespace Models.Request.Catalogo.Sucursales
{
    public class EntSucursalRequest
    {
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("Ciudad")]
        public string sCiudad { get; set; }
    }
}
