using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Seguridad
{
    public class PaginaRequest
    {
        public Guid? uIdPagina { get; set; }
        public Guid uIdModulo { get; set; }
        public string sClavePagina { get; set; }
        public string sNombrePagina { get; set; }
        public string sPathPagina { get; set; }
        public bool bMostrarEnMenu { get; set; }
    }
}
