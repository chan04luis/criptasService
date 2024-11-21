using Data.cs.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Seguridad.Cliente
{
    internal class ClientesRepositorio : IClientesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContext;
        public ClientesRepositorio(ApplicationDbContext dbContext, IHttpContextAccessor httpContext)
        {
            this.dbContext = dbContext;
            this.httpContext = httpContext;
        }

        public async Task<Response<Clientes>> DSave(Clientes newItem)
        {
            var response = new Response<Clientes>();
            try
            {
                newItem.id = Guid.NewGuid();
                newItem.fecha_registro = DateTime.Now;
                dbContext.Clientes.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    return default;
                }
                response.SetSuccess(newItem);

            }
            catch(Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<Clientes>> DGet(Guid iKey)
        {
            var response = new Response<Clientes>();
            try
            {
                var clientes = await dbContext.Clientes.AsNoTracking()
                .SingleOrDefaultAsync(x => x.id == iKey);
                response.SetSuccess(clientes);
            }
            catch(Exception ex)
            {
                throw;
            }
            return response;
        }

        public async Task<Response<Clientes>> DGet(string nombre, Guid id)
        {
            var response = new Response<Clientes>();
            try
            {
                var cliente = await dbContext.Clientes.SingleOrDefaultAsync(c => c.id == id && c.nombre == nombre);
                response.SetSuccess(cliente);
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
                
        }
    }
}
