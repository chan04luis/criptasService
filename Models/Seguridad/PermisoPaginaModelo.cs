using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PermisoPaginaModelo
    {
        [JsonPropertyName("IdPermisoPagina")]
        public Guid IdPermisoPagina { get; set; }
        [JsonPropertyName("IdPagina")]
        public Guid IdPagina { get; set; }
        [JsonPropertyName("ClavePagina")]
        public string? ClavePagina { get; set; }
        [JsonPropertyName("NombrePagina")]
        public string? NombrePagina { get; set; }
        [JsonPropertyName("TienePermiso")]
        public bool TienePermiso { get; set; }
        [JsonPropertyName("PermisosBoton")]
        public List<PermisoBotonModelo>? PermisosBoton { get; set; }
    }
}
