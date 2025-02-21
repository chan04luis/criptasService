
using Newtonsoft.Json;

namespace Models.Request.Usuarios
{
    public class EntChangePassword
    {
        [JsonProperty("Email")]
        public string? sCorreo { get; set; }

        [JsonProperty("Contra")]
        public string? sContra { get; set; }

        [JsonProperty("NewContra")]
        public string? sNContra { get; set; }

        [JsonProperty("ConfirmNewContra")]
        public string? sNCContra { get; set; }
    }
}
