using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class Modulo
    {
        public Guid uIdModulo { get; set; }
        public string? sClaveModulo { get; set; }
        public string sNombreModulo { get; set; }
        public string? sPathModulo { get; set; }
        public bool bMostrarEnMenu { get; set; }
        public bool bActivo { get; set; }
        public ICollection<Pagina> lstPaginas { get; set; }
        public ICollection<PermisoModulos> lstPermisosModulos { get; set; }
    }
}
