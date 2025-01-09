using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Seguridad
{
    public class ModuloRequest
    {
        [JsonProperty("IdModulo")]
        public Guid? uIdModulo { get; set; }
        [JsonProperty("ClaveModulo")]
        public string sClaveModulo { get; set; }
        [JsonProperty("NombreModulo")]
        public string sNombreModulo { get; set; }
        [JsonProperty("PathModulo")]
        public string sPathModulo { get; set; }
        [JsonProperty("MostrarEnMenu")]
        public bool bMostrarEnMenu { get; set; }
    }
}
