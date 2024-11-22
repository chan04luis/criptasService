using Newtonsoft.Json;

namespace Entities.JsonRequest.Clientes
{
    public class EntClienteRequest
    {
        [JsonProperty("Nombres")]
        public string sNombre { get; set; }
        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }
        [JsonProperty("Direccion")]
        public string sDireccion { get; set; }
        [JsonProperty("Telefono")]
        public string sTelefono { get; set; }
        [JsonProperty("Email")]
        public string sEmail { get; set; }
        [JsonProperty("FechaNac")]
        public string sFechaNac { get; set; }
        [JsonProperty("Sexo")]
        public string sSexo { get; set; }
    }
}
