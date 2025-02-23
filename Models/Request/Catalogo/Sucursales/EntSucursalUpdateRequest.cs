using Newtonsoft.Json;

namespace Models.Request.Catalogo.Sucursales
{
    public class EntSucursalUpdateRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }
        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("Ciudad")]
        public string sCiudad { get; set; }
    }
}
