using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Criptas
{
    public class EntCriptaSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }
        [JsonProperty("IdIglesia")]
        public Guid? uIdIglesia { get; set; }
        [JsonProperty("IdZona")]
        public Guid? uIdZona { get; set; }

        [JsonProperty("IdSeccion")]
        public Guid? uIdSeccion { get; set; }

        [JsonProperty("IdCliente")]
        public Guid? uIdCliente { get; set; }

        [JsonProperty("Numero")]
        public string? sNumero { get; set; }

        [JsonProperty("UbicacionEspecifica")]
        public string? sUbicacionEspecifica { get; set; }

        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
        [JsonProperty("NumPag")]
        public int iNumPag { get; set; }
        [JsonProperty("NumReg")]
        public int iNumReg { get; set; }
    }
}
