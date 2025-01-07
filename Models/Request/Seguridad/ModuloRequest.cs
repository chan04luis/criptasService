using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Seguridad
{
    public class ModuloRequest
    {
        public Guid? uIdModulo { get; set; }
        public string sClaveModulo { get; set; }
        public string sNombreModulo { get; set; }
        public string sPathModulo { get; set; }
        public bool bMostrarEnMenu { get; set; }
    }
}
