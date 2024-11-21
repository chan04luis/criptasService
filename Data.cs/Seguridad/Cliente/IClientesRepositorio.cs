using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.cs.Entities;
using Data.cs.Mapping;


namespace Data.cs.Seguridad.Cliente
{
    public interface IClientesRepositorio
    {
        Task<Response<Clientes>> DSave(Clientes newItem);
        Task<Response<Clientes>> DGet(Guid iKey);
        Task<Response<Clientes>> DGet(string nombre, Guid id);
    }
}
