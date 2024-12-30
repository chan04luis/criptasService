using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class PerfilPermisosModelo
    {
        public PerfilModelo Perfil { get; set; }
        public List<PermisoModuloModelo> Permisos { get; set; }
    }
}
