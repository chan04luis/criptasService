using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Iglesias
{
    public class EntIglesiaDeleteRequest
    {
        [JsonProperty("Id")]
        public Guid uId { get; set; }
    }
}
