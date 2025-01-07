using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class AutenticacionModelo
    {
        [JsonProperty("Token")]
        public string sToken { get; set; }
        [JsonProperty("RefreshToken")]
        public string sRefreshToken { get; set; }
        public DateTime dtExpires { get; set; }
    }
}
