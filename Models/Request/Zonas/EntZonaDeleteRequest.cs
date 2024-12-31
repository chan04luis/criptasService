using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Zonas
{
    public class EntZonaDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
}
