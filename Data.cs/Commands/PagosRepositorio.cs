
using AutoMapper;
using Business.Data;
using Data.cs.Entities;
using Entities;
using Entities.Models;
using Entities.Request.Pagos;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Data.cs.Commands
{
    public class PagosRepositorio : IPagosRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public PagosRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<List<EntPagos>>> DGetByClienteId(Guid uIdCliente)
        {
            var response = new Response<List<EntPagos>>();

            try
            {
                var pagos = await dbContext.Pagos.AsNoTracking()
                    .Where(p => p.uIdClientes == uIdCliente && !p.bEliminado).AsNoTracking()
                    .ToListAsync();

                response.Result = _mapper.Map<List<EntPagos>>(pagos);
                return response;
            }
            catch (Exception ex)
            {
                response.SetError($"Error al consultar pagos por cliente: {ex.Message}");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntPagos>> DSave(EntPagos entity)
        {
            var response = new Response<EntPagos>();
            try
            {
                var newItem = _mapper.Map<Pagos>(entity);
                newItem.bEliminado = false;
                newItem.dMontoPagado = 0;
                dbContext.Pagos.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el pago");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntPagos>(newItem));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntPagos>> DUpdatePagado(EntPagosUpdatePagadoRequest entity)
        {
            var response = new Response<EntPagos>();
            try
            {
                var bEntity = dbContext.Pagos.AsNoTracking().FirstOrDefault(x => x.uId == entity.uIdPago && x.bEliminado==false);
                if (bEntity != null)
                {
                    bEntity.dMontoPagado = entity.dMontoPagado;
                    bEntity.dtFechaPago = entity.dtFechaPagado;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    bEntity.bPagado = entity.bPagado;
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntPagos>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Pago no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntPagos>> DUpdate(EntPagos entity)
        {
            var response = new Response<EntPagos>();
            try
            {
                var bEntity = dbContext.Pagos.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.uIdClientes = entity.uIdClientes;
                    bEntity.uIdCripta = entity.uIdCripta;
                    bEntity.uIdTipoPago = entity.uIdTipoPago;
                    bEntity.dMontoTotal = entity.dMontoTotal;
                    bEntity.dtFechaLimite = entity.dtFechaLimite;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntPagos>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Pago no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntPagos>> DUpdateStatus(EntPagosUpdateEstatusRequest entity)
        {
            var response = new Response<EntPagos>();
            try
            {
                var bEntity = dbContext.Pagos.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.bEstatus = entity.bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntPagos>(bEntity), "Estado actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Pago no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DUpdateEliminado(Guid uId)
        {
            var response = new Response<bool>();
            try
            {
                var bEntity = dbContext.Pagos.AsNoTracking().FirstOrDefault(x => x.uId == uId);
                if (bEntity != null)
                {
                    bEntity.bEliminado = true;
                    bEntity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(true, "Eliminado correctamente");
                    else
                    {
                        response.SetError("Registro no eliminado");
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Pago no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntPagos>> DGetById(Guid uId)
        {
            var response = new Response<EntPagos>();
            try
            {
                var entity = await dbContext.Pagos.AsNoTracking().SingleOrDefaultAsync(x => x.uId == uId && !x.bEliminado);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntPagos>(entity));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntPagos>>> DGetByFilters(EntPagosSearchRequest filtros)
        {
            var response = new Response<List<EntPagos>>();
            try
            {
                var query = dbContext.Pagos.AsNoTracking().Where(x => !x.bEliminado);

                if (filtros.uIdClientes.HasValue)
                    query = query.Where(x => x.uIdClientes == filtros.uIdClientes);

                if (filtros.uIdCripta.HasValue)
                    query = query.Where(x => x.uIdCripta == filtros.uIdCripta);

                if (filtros.uIdTipoPago.HasValue)
                    query = query.Where(x => x.uIdTipoPago == filtros.uIdTipoPago);

                if (filtros.bPagado.HasValue)
                    query = query.Where(x => x.bPagado == filtros.bPagado);

                if (filtros.bEstatus.HasValue)
                    query = query.Where(x => x.bEstatus == filtros.bEstatus);

                var items = await query.ToListAsync();

                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntPagos>>(items));
                else
                {
                    response.SetError("No se encontraron registros");
                    response.HttpCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }
    }
}
