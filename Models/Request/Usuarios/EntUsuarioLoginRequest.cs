using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Usuarios
{
    public class EntUsuarioLoginRequest
    {
        [JsonProperty("Correo")]
        public string? sCorreo { get; set; }

        [JsonProperty("Contra")]
        public string? sContra { get; set; }
    }
}
