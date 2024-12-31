using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class LoginResponseModelo
    {
        public UsuarioModelo Usuario { get; set; }
        public string Token { get; set; }
        public ConfiguracionModelo Configuracion { get; set; }
        public List<PermisoModuloModelo> PermisosModulos { get; set; }
        public List<PermisoPaginaModelo> PermisosPaginas { get; set; }
        public List<PermisoBotonModelo> PermisosBotones { get; set; }
        public object Menu { get; set; }
    }
}
