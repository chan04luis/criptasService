
using AutoMapper;
using Business.Data;
using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using Models.Enums;
using Models.Models;
using Models.Request.Pagos;
using Models.Responses.Pagos;
using System.Net;
using Utils;

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

        public async Task<Response<EntSolicitudPago>> DGetInfoById(Guid uId)
        {
            var response = new Response<EntSolicitudPago>();
            try
            {
                var entity = await dbContext.SolicitudPagos.AsNoTracking().SingleOrDefaultAsync(x => x.uIdPago == uId && x.bEstatus == null);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntSolicitudPago>(entity));
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

        public async Task<Response<List<EntPagos>>> DGetByClienteId(Guid uIdCliente, Guid uIdCripta)
        {
            var response = new Response<List<EntPagos>>();

            try
            {
                var pagos = await dbContext.Pagos.AsNoTracking()
                    .Where(p => p.uIdClientes == uIdCliente && !p.bEliminado && p.uIdCripta== uIdCripta).AsNoTracking()
                    .ToListAsync();

                response.SetSuccess(_mapper.Map<List<EntPagos>>(pagos));
                return response;
            }
            catch (Exception ex)
            {
                response.SetError($"Error al consultar pagos por cliente: {ex.Message}");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSolicitudPago>> DSaveInfoPago(EntSolicitudPago entity)
        {
            var response = new Response<EntSolicitudPago>();
            try
            {
                var newItem = _mapper.Map<SolicitudPago>(entity);
                newItem.bEstatus = null;
                newItem.dtFechaRegistro = DateTime.Now.ToLocalTime();
                newItem.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.SolicitudPagos.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el pago");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntSolicitudPago>(newItem));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntSolicitudPago>> DUpdateStatusInfoPago(EntSolicitudPago entity)
        {
            var response = new Response<EntSolicitudPago>();
            try
            {
                var bEntity = dbContext.SolicitudPagos.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.bEstatus = entity.bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntSolicitudPago>(bEntity), "Estado actualizado correctamente");
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
                    if (bEntity.bPagado)
                    {
                        response.SetError("No se puede eliminar una venta ya pagada");
                        response.HttpCode = HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        bEntity.bEliminado = true;
                        bEntity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                        dbContext.Update(bEntity);
                        if(bEntity.uIdCripta != new Guid(IdPermanentes.clienteGeneral.GetDescription()))
                        {
                            var cripta = await dbContext.Criptas.AsNoTracking().FirstOrDefaultAsync(x => x.uId == bEntity.uIdCripta);
                            cripta.bDisponible = true;
                            cripta.bEstatus = true;
                            cripta.uIdCliente = new Guid(IdPermanentes.clienteGeneral.GetDescription());
                            cripta.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                            dbContext.Update(cripta);
                        }
                        var exec = await dbContext.SaveChangesAsync();

                        if (exec > 0)
                            response.SetSuccess(true, "Eliminado correctamente");
                        else
                        {
                            response.SetError("Registro no eliminado");
                            response.HttpCode = HttpStatusCode.BadRequest;
                        }
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

        public async Task<Response<PagedResult<EntPagosLista>>> DGetByFilters(EntPagosSearchRequest filtros)
        {
            var response = new Response<PagedResult<EntPagosLista>>();
            try
            {
                var query = (from p in dbContext.Pagos.AsNoTracking()
                             join tp in dbContext.TiposDePagos.AsNoTracking() on p.uIdTipoPago equals tp.uId
                             join c in dbContext.Clientes.AsNoTracking() on p.uIdClientes equals c.uId
                             join cr in dbContext.Criptas.AsNoTracking() on p.uIdCripta equals cr.uId
                             join s in dbContext.Secciones.AsNoTracking() on cr.uIdSeccion equals s.uId
                             join z in dbContext.Zonas.AsNoTracking() on s.uIdZona equals z.uId
                             join i in dbContext.Iglesias.AsNoTracking() on z.uIdIglesia equals i.uId
                             where !p.bEliminado
                             select new EntPagosLista
                             {
                                 uId = p.uId,
                                 uIdClientes = c.uId,
                                 sNombreCliente = c.sNombre,
                                 sApellidosCliente = c.sApellidos ?? "",
                                 uIdCripta = cr.uId,
                                 sNumeroCripta = cr.sNumero,
                                 uIdSeccion = s.uId,
                                 sNombreSeccion = s.sNombre,
                                 uIdZona = z.uId,
                                 sNombreZona = z.sNombre,
                                 uIdIglesia = i.uId,
                                 sNombreIglesia = i.sNombre,
                                 uIdTipoPago = p.uIdTipoPago,
                                 dMontoTotal = p.dMontoTotal,
                                 dMontoPagado = p.dMontoPagado,
                                 dtFechaLimite = p.dtFechaLimite,
                                 dtFechaPagado = p.dtFechaPago,
                                 bPagado = p.bPagado,
                                 dtFechaRegistro = p.dtFechaRegistro,
                                 dtFechaActualizacion = p.dtFechaActualizacion,
                                 bEstatus = p.bEstatus,
                                 sClavePago = tp.sNombre
                             });

                // Aplicar filtros dinámicos
                if (filtros.sCliente != null)
                    query = query.Where(p => ($"{p.sNombreCliente} {p.sApellidosCliente}").Contains(filtros.sCliente));

                if (filtros.uIdCripta.HasValue)
                    query = query.Where(p => p.uIdCripta == filtros.uIdCripta);

                if (filtros.uIdSeccion.HasValue)
                    query = query.Where(p => p.uIdSeccion == filtros.uIdSeccion);

                if (filtros.uIdZona.HasValue)
                    query = query.Where(p => p.uIdZona == filtros.uIdZona);

                if (filtros.uIdIglesia.HasValue)
                    query = query.Where(p => p.uIdIglesia == filtros.uIdIglesia);

                if (filtros.uIdTipoPago.HasValue)
                    query = query.Where(p => p.uIdTipoPago == filtros.uIdTipoPago);

                if (filtros.bPagado.HasValue)
                    query = query.Where(p => p.bPagado == filtros.bPagado);

                if (filtros.bEstatus.HasValue)
                    query = query.Where(p => p.bEstatus == filtros.bEstatus);

                // Paginación
                int totalRecords = await query.CountAsync();
                var resultList = await query
                    .OrderBy(p => p.sNombreIglesia)
                    .ThenBy(p => p.sNombreZona)
                    .ThenBy(p => p.sNombreSeccion)
                    .ThenBy(p => p.sNumeroCripta)
                    .Skip((filtros.iNumPag - 1) * filtros.iNumReg)
                    .Take(filtros.iNumReg)
                    .ToListAsync();

                // Verificar si hay registros
                if (resultList.Any())
                {
                    var resultado = new PagedResult<EntPagosLista>(resultList, totalRecords, filtros.iNumPag, filtros.iNumReg);
                    response.SetSuccess(resultado);
                }
                else
                {
                    response.SetError("No se encontraron registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
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
