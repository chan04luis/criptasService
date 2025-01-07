using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class LoginResponseModelo
    {
        [JsonProperty("Usuario")]
        public UsuarioModelo Usuario { get; set; }
        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("Configuracion")]
        public ConfiguracionModelo Configuracion { get; set; }
        [JsonProperty("PermisosModulos")]
        public List<PermisoModuloModelo> PermisosModulos { get; set; }
        [JsonProperty("PermisosPaginas")]
        public List<PermisoPaginaModelo> PermisosPaginas { get; set; }
        [JsonProperty("PermisosBotones")]
        public List<PermisoBotonModelo> PermisosBotones { get; set; }
        [JsonProperty("Menu")]
        public object Menu { get; set; }
    }
}
