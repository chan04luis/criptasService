using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PermisoModuloModelo
    {
        [JsonProperty("IdPermisoModulo")]
        public Guid IdPermisoModulo { get; set; }
        [JsonProperty("IdModulo")]
        public Guid IdModulo { get; set; }
        [JsonProperty("ClaveModulo")]
        public string? ClaveModulo { get; set; }
        [JsonProperty("NombreModulo")]
        public string? NombreModulo { get; set; }
        [JsonProperty("TienePermiso")]
        public bool TienePermiso { get; set; }
        [JsonProperty("PermisosPagina")]
        public List<PermisoPaginaModelo>? PermisosPagina { get; set; }
    }
}
