using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Fallecidos
{
    public class EntFallecidosRequest
    {

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }

        [JsonProperty("IdCripta")]
        public Guid? uIdCripta { get; set; }
    }
}
