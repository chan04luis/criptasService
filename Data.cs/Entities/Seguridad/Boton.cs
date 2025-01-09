using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class Boton
    {
        public Guid uIdBoton { get; set; }
        public Guid uIdPagina { get; set; }
        public string sClaveBoton { get; set; }
        public string sNombreBoton { get; set; }
        public bool bActivo { get; set; }
        public Pagina pagina { get; set; }  
        public ICollection<PermisoBotones> lstPermisosBotones { get; set; }
    }
}
