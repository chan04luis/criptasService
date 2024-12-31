using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Visitas
{
    public class EntVisitasRequest
    {
        [JsonProperty("NombreVisitante")]
        public string sNombreVisitante { get; set; }

        [JsonProperty("IdCriptas")]
        public Guid uIdCriptas { get; set; }
    }
}
