using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class Configuracion
    {
        public Guid uIdConfiguracion { get; set; }
        public string? sTituloNavegador { get; set; }
        public string? sTitulo { get; set; }
        public string? sMetaDescripcion { get; set; }
        public string? sColorPrimario { get; set; }
        public string? sColorSecundario { get; set; }
        public string? sContrastePrimario { get; set; }
        public string? sContrasteSecundario { get; set; }
        public string? sUrlFuente { get; set; }
        public string? sNombreFuente { get; set; }
        public string? sRutaImagenFondo { get; set; }
        public string? sRutaImagenLogo { get; set; }
        public string? sRutaImagenPortal { get; set; }

        public DateTime? dtFechaCreacion { get; set; }
        public Guid? uIdUsuarioCreacion { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public Guid? uIdUsuarioModificacion { get; set; }
        public DateTime? dtFechaEliminacion { get; set; }
        public Guid? uIdUsuarioEliminacion { get; set; }
        public bool bActivo { get; set; }
        public bool? bBaja { get; set; }
    }
}
