using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PaginaModelo
    {
        [JsonProperty("IdPagina")]
        public Guid? uIdPagina { get; set; }
        [JsonProperty("IdModulo")]
        public Guid? uIdModulo { get; set; }
        [JsonProperty("ClavePagina")]
        public string? sClavePagina { get; set; }
        [JsonProperty("NombrePagina")]
        public string? sNombrePagina { get; set; }
        [JsonProperty("PathPagina")]
        public string? sPathPagina { get; set; }
        [JsonProperty("MostrarEnMenu")]
        public bool? bMostrarEnMenu { get; set; }
        [JsonProperty("Botones")]
        public List<BotonModelo>? Botones { get; set; }
    }
}
