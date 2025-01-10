using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PermisoModuloModelo
    {
        [JsonPropertyName("IdPermisoModulo")]
        public Guid IdPermisoModulo { get; set; }
        [JsonPropertyName("IdModulo")]
        public Guid IdModulo { get; set; }
        [JsonPropertyName("ClaveModulo")]
        public string? ClaveModulo { get; set; }
        [JsonPropertyName("NombreModulo")]
        public string? NombreModulo { get; set; }
        [JsonPropertyName("TienePermiso")]
        public bool TienePermiso { get; set; }
        [JsonPropertyName("PermisosPagina")]
        public List<PermisoPaginaModelo>? PermisosPagina { get; set; }
    }
}
