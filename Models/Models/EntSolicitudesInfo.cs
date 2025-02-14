using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class EntSolicitudesInfo
    {
        public Guid Id { get; set; }

        public Guid IdCliente { get; set; }

        public Guid IdServicio { get; set; }

        public string? Mensaje { get; set; }

        public bool Visto { get; set; } = false;

        public bool Atendido { get; set; } = false;

    }
}
