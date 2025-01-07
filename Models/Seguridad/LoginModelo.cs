using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class LoginModelo
    {
        [JsonProperty("Correo")]
        public required string sCorreo { get; set; }
        [JsonProperty("Password")]
        public required string sPassword { get; set; }
    }
}
