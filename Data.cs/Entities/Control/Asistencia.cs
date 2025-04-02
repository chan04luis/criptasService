using System;

namespace Data.cs.Entities.Control
{
    public class Asistencia
    {
        public Guid Id { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdGrupo { get; set; }

        public DateTime Fecha { get; set; }

        public string Tipo { get; set; } = null!; // 'Entrada', 'Finalizado', 'Asistencia', 'Retardo'

        public DateTime HoraRegistro { get; set; }
    }
}
