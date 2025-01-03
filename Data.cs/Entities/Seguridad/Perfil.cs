using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities.Seguridad
{
    public class Perfil
    {
        public Guid? id { get; set; }
        public string ClavePerfil { get; set; }
        public string NombrePerfil { get; set; }
        public bool Eliminable { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Guid UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public Guid UsuarioModificacion { get; set; }
        public bool Activo { get; set; }
    }
}
