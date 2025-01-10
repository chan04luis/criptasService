using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class GuardarPermisosModelo
    {
        [JsonPropertyName("IdPerfil")]
        public Guid IdPerfil { get; set; }
        [JsonPropertyName("Permisos")]
        public List<PermisoModuloModelo>? Permisos { get; set; }
    }
}
