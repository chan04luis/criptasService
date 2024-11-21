using Data.cs.Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.CasosUso.Clientes
{
    public interface IClientesCasosUso
    {
        Task<Response<ClientesModelo>> BCreaate(ClientesModelo clientesModel);
    }
}
