using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class AutenticacionModelo
    {
        public string sToken { get; set; }
        [JsonProperty("RefreshToken")]
        public string sRefreshToken { get; set; }
        public DateTime dtExpires { get; set; }
    }
}
