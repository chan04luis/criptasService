using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Beneficiarios
{
    public class EntBeneficiariosRequest
    {
        [JsonProperty("IdCripta")]
        public Guid uIdCripta { get; set; }

        [JsonProperty("Nombre")]
        public string sNombre { get; set; }
    }
}
