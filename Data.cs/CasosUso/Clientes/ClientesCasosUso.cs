using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.cs.Modelos.Seguridad;

namespace Data.cs.CasosUso.Clientes
{
    internal class ClientesCasosUso : IClientesCasosUso
    {
        private readonly IMapper mapeador;
        private readonly IClientesCasosUso repositorio;

        public ClientesCasosUso(IMapper mapeador, IClientesCasosUso repositorio)
        {
            this.mapeador = mapeador;
            this.repositorio = repositorio;
        }

        public Task<Response<ClientesCasosUso>> BCreaate(ClientesCasosUso clientesCasosUso)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ClientesModelo>> BCreaate(ClientesModelo clientesModel)
        {
            throw new NotImplementedException();
        }
    }
}
