using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Seguridad
{
    public class PaginaModelo
    {
        public Guid? uIdPagina { get; set; }
        public Guid? uIdModulo { get; set; }
        public string? sClavePagina { get; set; }
        public string? sNombrePagina { get; set; }
        public string? sPathPagina { get; set; }
        public bool? bMostrarEnMenu { get; set; }
        public List<BotonModelo>? Botones { get; set; }
        public bool bActivo { get; set; }
    }
}
