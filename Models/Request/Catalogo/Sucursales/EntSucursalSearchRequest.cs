using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Catalogo.Sucursales
{
    public class EntSucursalSearchRequest
    {
        [JsonProperty("Id")]
        public Guid? uId { get; set; }
        [JsonProperty("Nombre")]
        public string? sNombre { get; set; }
        [JsonProperty("Ciudad")]
        public string? sCiudad { get; set; }
        [JsonProperty("Estatus")]
        public bool? bEstatus { get; set; }
    }
}
