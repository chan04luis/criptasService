using Newtonsoft.Json;

namespace Models.Seguridad
{
    public class LoginModelo
    {
        [JsonProperty("Correo")]
        public required string sCorreo { get; set; }
        [JsonProperty("Password")]
        public required string sPassword { get; set; }
        [JsonProperty("TokenFireBase")]
        public required string? sTokenFireBase { get; set; }
    }
}
