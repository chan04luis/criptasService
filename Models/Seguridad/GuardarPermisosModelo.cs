using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class GuardarPermisosModelo
    {
        [JsonProperty("IdPerfil")]
        public Guid IdPerfil { get; set; }
        [JsonProperty("Permisos")]
        public List<PermisoModuloModelo>? Permisos { get; set; }
    }
}
