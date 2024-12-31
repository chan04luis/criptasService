using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Seguridad
{
    public class PerfilPermisoMenuModelo
    {
        public Guid? IdModulo { get; set; }
        public string? ClaveModulo { get; set; }
        public string? NombreModulo { get; set; }
        public string? PathModulo { get; set; }
        public bool MostrarModuloEnMenu { get; set; }
        public Guid? IdPagina { get; set; }
        public string? ClavePagina { get; set; }
        public string? NombrePagina { get; set; }
        public string? PathPagina { get; set; }
        public bool? MostrarPaginaEnMenu { get; set; }
    }
}
