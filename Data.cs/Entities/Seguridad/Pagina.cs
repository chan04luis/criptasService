using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class Pagina
    {
        public Guid uIdPagina { get; set; }
        public Guid uIdModulo { get; set; }
        public string? sClavePagina { get; set; }
        public string sNombrePagina { get; set; }
        public string? sPathPagina { get; set; }
        public bool? bMostrarEnMenu { get; set; }
        public bool bActivo { get; set; }
        public Modulo Modulo { get; set; }
        public ICollection<Boton> lstBotones { get; set; }
        public ICollection<PermisosPagina> lstPermisosPaginas { get; set; }
    }
}
