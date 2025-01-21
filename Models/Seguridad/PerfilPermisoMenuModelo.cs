using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PerfilPermisoMenuModelo
    {
        [JsonProperty("IdModulo")]
        public Guid? IdModulo { get; set; }
        [JsonProperty("ClaveModulo")]
        public string? ClaveModulo { get; set; }
        [JsonProperty("NombreModulo")]
        public string? NombreModulo { get; set; }
        [JsonProperty("PathModulo")]
        public string? PathModulo { get; set; }
        [JsonProperty("MostrarModuloEnMenu")]
        public bool MostrarModuloEnMenu { get; set; }

        public List<PaginaModelo>? Paginas { get; set; }    
    }
}
