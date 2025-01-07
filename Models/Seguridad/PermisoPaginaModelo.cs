using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PermisoPaginaModelo
    {
        [JsonProperty("IdPermisoPagina")]
        public Guid IdPermisoPagina { get; set; }
        [JsonProperty("IdPagina")]
        public Guid IdPagina { get; set; }
        [JsonProperty("ClavePagina")]
        public string? ClavePagina { get; set; }
        [JsonProperty("NombrePagina")]
        public string? NombrePagina { get; set; }
        [JsonProperty("TienePermiso")]
        public bool TienePermiso { get; set; }
        [JsonProperty("PermisosBoton")]
        public List<PermisoBotonModelo>? PermisosBoton { get; set; }
    }
}
