using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request.Seguridad
{
    public class BotonRequest
    {
        public Guid? uIdBoton { get; set; }
        public Guid uIdPagina { get; set; }
        public string sClaveBoton { get; set; }
        public string sNombreBoton { get; set; }
    }
}
