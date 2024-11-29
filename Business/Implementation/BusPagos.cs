using AutoMapper;
using Business.Data;
using Business.Interfaces;
using Entities.Models;
using Entities.Request.Pagos;
using Entities;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Business.Implementation
{
    public class BusPagos : IBusPagos
    {
        private readonly IPagosRepositorio _pagosRepositorio;
        private readonly IPagosParcialesRepositorio _parcialesRepositorio;
        private readonly ILogger<BusPagos> _logger;
        private readonly IMapper _mapper;

        public BusPagos(IPagosRepositorio pagosRepositorio, ILogger<BusPagos> logger, IMapper mapper, IPagosParcialesRepositorio parcialesRepositorio)
        {
            _pagosRepositorio = pagosRepositorio;
            _logger = logger;
            _mapper = mapper;
            _parcialesRepositorio = parcialesRepositorio;
        }

        public async Task<Response<EntPagos>> ValidateAndSavePago(EntPagosRequest pago)
        {
            var response = new Response<EntPagos>();

            try
            {
                if (pago.dMontoTotal <= 0)
                {
                    response.SetError("El monto total debe ser mayor que cero.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _pagosRepositorio.DGetByClienteId(pago.uIdClientes);
                if (!item.HasError && item.Result.Any(p => p.uIdCripta == pago.uIdCripta && p.uIdTipoPago == pago.uIdTipoPago))
                {
                    response.SetError("Ya existe un pago registrado con esos datos.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }

                EntPagos nuevoPago = new EntPagos
                {
                    uId = Guid.NewGuid(),
                    uIdClientes = pago.uIdClientes,
                    uIdCripta = pago.uIdCripta,
                    uIdTipoPago = pago.uIdTipoPago,
                    dMontoTotal = pago.dMontoTotal,
                    dtFechaLimite = pago.dtFechaLimite,
                    bPagado = false,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                return await SavePago(nuevoPago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar el pago");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntPagos>> SavePago(EntPagos pago)
        {
            try
            {
                return await _pagosRepositorio.DSave(_mapper.Map<EntPagos>(pago));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el pago");
                var response = new Response<EntPagos>();
                response.SetError("Hubo un error al guardar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntPagos>> ValidateAndUpdatePago(Guid uId, EntPagosRequest pago)
        {
            var response = new Response<EntPagos>();

            try
            {
                if (pago.dMontoTotal <= 0)
                {
                    response.SetError("El monto total debe ser mayor que cero.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _pagosRepositorio.DGetByClienteId(pago.uIdClientes);
                if (!item.HasError && item.Result.Any(p => p.uIdCripta == pago.uIdCripta && p.uIdTipoPago == pago.uIdTipoPago && p.uId != uId))
                {
                    response.SetError("Ya existe un pago registrado con esos datos.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }
                
                var pagoActualizado = _mapper.Map<EntPagos>(pago);
                pagoActualizado.uId = uId;
                return await UpdatePago(pagoActualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar el pago");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntPagos>> UpdatePago(EntPagos pago)
        {
            try
            {
                return await _pagosRepositorio.DUpdate(pago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pago");
                var response = new Response<EntPagos>();
                response.SetError("Hubo un error al actualizar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntPagos>> UpdatePagado(ReadPagosRequest pago)
        {
            var response = new Response<EntPagos>();
            try
            {
                decimal dMonto = 0, dMontoActual=0;
                if (decimal.TryParse(pago.dMontoPagado.ToString(), out dMonto))
                {
                    if(dMonto > 0)
                    {
                        EntPagosUpdatePagadoRequest entidad = new EntPagosUpdatePagadoRequest();
                        entidad.uIdPago = pago.uIdPago;
                        var consulta = await _pagosRepositorio.DGetById(pago.uIdPago);
                        var pagoBD = consulta.Result;
                        if (pagoBD == null)
                        {
                            response.SetError("No se encontro el pago.");
                            response.HttpCode = HttpStatusCode.InternalServerError;
                        }
                        else if (pagoBD.bPagado == true)
                        {
                            response.SetError("Ya ha sido pagado.");
                            response.HttpCode = HttpStatusCode.InternalServerError;
                        }
                        else
                        {
                            if (decimal.TryParse(pagoBD.dMontoPagado.ToString(), out dMontoActual))
                                entidad.dMontoPagado = dMonto + dMontoActual;
                            else
                                entidad.dMontoPagado = dMonto;

                            entidad.bPagado = (pagoBD.dMontoTotal <= entidad.dMontoPagado);
                            entidad.dtFechaPagado = entidad.bPagado ? DateTime.Now.ToLocalTime() : null;
                            if (!entidad.bPagado || pagoBD.dMontoTotal > pago.dMontoPagado)
                            {
                                EntPagosParciales entPagosParciales = new EntPagosParciales()
                                {
                                    uIdPago = entidad.uIdPago,
                                    dMonto = pago.dMontoPagado,
                                    dtFechaActualizacion=DateTime.Now.ToLocalTime(),
                                    dtFechaPago=DateTime.Now.ToLocalTime(),
                                    dtFechaRegistro=DateTime.Now.ToLocalTime(),
                                    bEstatus=true,
                                    uId=Guid.NewGuid()
                                };
                                await _parcialesRepositorio.DSave(entPagosParciales);
                            }
                            response = await _pagosRepositorio.DUpdatePagado(entidad);
                        }
                    }
                    else
                    {
                        response.SetError("El monto no puede ser 0.");
                        response.HttpCode = HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    response.SetError("No se reconoce el monto de pago.");
                    response.HttpCode = HttpStatusCode.InternalServerError;
                }
                
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pago");
                response.SetError("Hubo un error al actualizar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
        public async Task<Response<EntPagos>> UpdateCancelarPagado(Guid uIdPago)
        {
            var response = new Response<EntPagos>();
            try
            {
                EntPagosUpdatePagadoRequest entidad = new EntPagosUpdatePagadoRequest();
                entidad.uIdPago = uIdPago;
                entidad.bPagado = false;
                entidad.dMontoPagado = 0;
                entidad.dtFechaPagado = null;
                var consulta = await _pagosRepositorio.DGetById(uIdPago);
                var pagoBD = consulta.Result;
                if (pagoBD == null)
                {
                    response.SetError("No se encontro el pago.");
                    response.HttpCode = HttpStatusCode.InternalServerError;
                }
                else
                {
                    await _parcialesRepositorio.DUpdateEliminadoByIdPago(uIdPago);
                    response = await _pagosRepositorio.DUpdatePagado(entidad);
                }

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el pago");
                response.SetError("Hubo un error al actualizar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntPagos>> UpdatePagoStatus(EntPagosUpdateEstatusRequest pago)
        {
            try
            {
                return await _pagosRepositorio.DUpdateStatus(pago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del pago");
                var response = new Response<EntPagos>();
                response.SetError("Hubo un error al actualizar el estado del pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeletePagoById(Guid id)
        {
            try
            {
                return await _pagosRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pago por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeletePagoParcialById(Guid id)
        {
            try
            {
                var getPagoParcial = await _parcialesRepositorio.DGetById(id);
                var pagoParcial = getPagoParcial.Result;
                var getPago = await _pagosRepositorio.DGetById(pagoParcial.uIdPago);
                var pago = getPago.Result;
                EntPagosUpdatePagadoRequest uPago = new EntPagosUpdatePagadoRequest();
                uPago.bPagado = false;
                uPago.dMontoPagado = pago.dMontoPagado - pagoParcial.dMonto;
                uPago.dtFechaPagado = null;
                uPago.uIdPago = pago.uId;
                await _pagosRepositorio.DUpdatePagado(uPago);
                return await _parcialesRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el pago por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntPagos>> GetPagoById(Guid id)
        {
            try
            {
                return await _pagosRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pago por ID");
                var response = new Response<EntPagos>();
                response.SetError("Hubo un error al obtener el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntPagos>>> GetPagosByFilters(EntPagosSearchRequest filtros)
        {
            try
            {
                return await _pagosRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los pagos por filtros");
                var response = new Response<List<EntPagos>>();
                response.SetError("Hubo un error al obtener los pagos.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntPagosParciales>>> GetParcialidadesByIdPago(Guid id)
        {
            try
            {
                return await _parcialesRepositorio.DGetByPagoId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pago por ID");
                var response = new Response<List<EntPagosParciales>>();
                response.SetError("Hubo un error al obtener el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
