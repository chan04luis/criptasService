using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses.Usuarios
{
    public class AuthLogin
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }

        [JsonProperty("Nombres")]
        public string sNombres { get; set; }

        [JsonProperty("Apellidos")]
        public string sApellidos { get; set; }

        [JsonProperty("Correo")]
        public string sCorreo { get; set; }

        [JsonProperty("Token")]
        public string sToken { get; set; }
    }
}
