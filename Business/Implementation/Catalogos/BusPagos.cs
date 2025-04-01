using AutoMapper;
using Business.Data;
using Microsoft.Extensions.Logging;
using System.Net;
using Business.Interfaces.Catalogos;
using Utils;
using Models.Models;
using Models.Request.Pagos;
using Models.Responses.Pagos;
using Data.cs.Entities.Catalogos;
using Models.Enums;
using Utils.Implementation;

namespace Business.Implementation.Catalogos
{
    public class BusPagos : IBusPagos
    {
        private readonly IClientesRepositorio _cliente;
        private readonly IPagosRepositorio _pagosRepositorio;
        private readonly ICriptasRepositorio _criptasRepositorio;   
        private readonly IPagosParcialesRepositorio _parcialesRepositorio;
        private readonly ILogger<BusPagos> _logger;
        private readonly IMapper _mapper;
        private readonly FirebaseNotificationService _firebase;
        private readonly EmailService _emailService;

        public BusPagos(IPagosRepositorio pagosRepositorio, ILogger<BusPagos> logger, IMapper mapper, IPagosParcialesRepositorio parcialesRepositorio, 
            ICriptasRepositorio criptasRepositorio, FirebaseNotificationService firebase, EmailService emailService, IClientesRepositorio cliente)
        {
            _pagosRepositorio = pagosRepositorio;
            _logger = logger;
            _mapper = mapper;
            _parcialesRepositorio = parcialesRepositorio;
            _criptasRepositorio = criptasRepositorio;
            _emailService = emailService;
            _firebase = firebase;
            _cliente = cliente;
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

                var cripta = await _criptasRepositorio.DGetById(pago.uIdCripta);
                if (cripta.HasError || cripta.Result == null && pago.iTipoPago == 1)
                {
                    response.SetError("No existe el cripta.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }else if(!cripta.Result.bEstatus || !cripta.Result.bDisponible && pago.iTipoPago == 1)
                {
                    response.SetError("Cripta no disponible para su apartado.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var clienteResponse = await _cliente.DGetById(pago.uIdClientes);
                if (clienteResponse.HasError || clienteResponse.Result == null)
                {
                    response.SetError("No existe el cliente.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _pagosRepositorio.DGetByClienteId(pago.uIdClientes, pago.uIdCripta);
                if (!item.HasError && pago.iTipoPago == 1 && item.Result.Any(p => p.uIdCripta == pago.uIdCripta && p.uIdTipoPago == pago.uIdTipoPago))
                {
                    response.SetError("Ya existe un pago registrado con esos datos.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }

                EntPagos nuevoPago = new EntPagos
                {
                    uId = Guid.NewGuid(),
                    uIdClientes = pago.uIdClientes,
                    iTipoPago = pago.iTipoPago,
                    uIdCripta = pago.uIdCripta,
                    uIdTipoPago = pago.uIdTipoPago,
                    dMontoTotal = pago.dMontoTotal,
                    dtFechaLimite = pago.dtFechaLimite,
                    bPagado = false,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                var responseFinal = await SavePago(nuevoPago);
                if (!responseFinal.HasError)
                {
                    await _emailService.EnviarCorreoPago(clienteResponse.Result, "CREADO", nuevoPago.dMontoTotal, cripta.Result.sNumero, pago.iTipoPago);
                    await _firebase.EnviarNotificacionAsync(
                        clienteResponse.Result.sFcmToken,
                        "Pago Creado",
                        $"Tu cripta ha sido asignada con éxito. Monto {pago.dMontoTotal:C} con Fecha Limite de {pago.dtFechaLimite}"
                    );
                }
                return responseFinal;
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
                var respuesta = await _pagosRepositorio.DSave(_mapper.Map<EntPagos>(pago));
                if(!respuesta.HasError)
                {
                    if(pago.uIdCripta != new Guid(IdPermanentes.clienteGeneral.GetDescription()) && pago.iTipoPago == 1)
                    {
                        EntCriptas entity = new EntCriptas()
                        {
                            uId = pago.uIdCripta,
                            bEstatus = false,
                            bDisponible = true,
                            uIdCliente = pago.uIdClientes
                        };
                        await _criptasRepositorio.DUpdateBooleanByApp(entity);
                    }
                }
                return respuesta;
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

                var cripta = await _criptasRepositorio.DGetById(pago.uIdCripta);
                if (cripta.HasError || cripta.Result == null)
                {
                    response.SetError("No existe el cripta.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }
                else if (!cripta.Result.bEstatus || !cripta.Result.bDisponible)
                {
                    response.SetError("Cripta no disponible para su apartado.");
                    response.HttpCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _pagosRepositorio.DGetByClienteId(pago.uIdClientes, pago.uIdCripta);
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
                decimal dMonto = 0, dMontoActual = 0;
                if (decimal.TryParse(pago.dMontoPagado.ToString(), out dMonto))
                {
                    if (dMonto > 0)
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
                            var clienteResponse = await _cliente.DGetById(pagoBD.uIdClientes);
                            if (clienteResponse.HasError || clienteResponse.Result == null)
                            {
                                response.SetError("No existe el cliente.");
                                response.HttpCode = HttpStatusCode.BadRequest;
                                return response;
                            }
                            var cripta = await _criptasRepositorio.DGetById(pagoBD.uIdCripta);
                            if (cripta.HasError || cripta.Result == null)
                            {
                                response.SetError("No existe el cripta.");
                                response.HttpCode = HttpStatusCode.BadRequest;
                                return response;
                            }
                            if (decimal.TryParse(pagoBD.dMontoPagado.ToString(), out dMontoActual))
                                entidad.dMontoPagado = dMonto + dMontoActual;
                            else
                                entidad.dMontoPagado = dMonto;

                            entidad.bPagado = pagoBD.dMontoTotal <= entidad.dMontoPagado;
                            if (pago.bApplyDate)
                            {
                                if (DateTime.TryParse(pago.sFechaPagado, out DateTime fechaP))
                                {
                                    entidad.dtFechaPagado = fechaP;
                                }
                                else
                                {
                                    response.SetError("La fecha es incorrecta.");
                                    response.HttpCode = HttpStatusCode.BadRequest;
                                    return response;
                                }
                            }
                            else
                                entidad.dtFechaPagado = entidad.bPagado ? DateTime.Now.ToLocalTime() : null;
                            if (!entidad.bPagado || pagoBD.dMontoTotal > pago.dMontoPagado)
                            {
                                EntPagosParciales entPagosParciales = new EntPagosParciales()
                                {
                                    uIdPago = entidad.uIdPago,
                                    dMonto = pago.dMontoPagado,
                                    dtFechaActualizacion = DateTime.Now.ToLocalTime(),
                                    dtFechaPago = pago.bApplyDate ? DateTime.Parse(pago.sFechaPagado) : DateTime.Now.ToLocalTime(),
                                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                                    bEstatus = true,
                                    uId = Guid.NewGuid()
                                };
                                await _parcialesRepositorio.DSave(entPagosParciales);
                            }
                            response = await _pagosRepositorio.DUpdatePagado(entidad);
                            if (entidad.bPagado && !response.HasError)
                            {
                                if (pagoBD.uIdCripta != new Guid(IdPermanentes.clienteGeneral.GetDescription()))
                                {
                                    if(pagoBD.iTipoPago == 1)
                                    {
                                        EntCriptas entity = new EntCriptas()
                                        {
                                            uId = pagoBD.uIdCripta,
                                            bDisponible = false,
                                            uIdCliente = pagoBD.uIdClientes,
                                            dtFechaPagado = pago.bApplyDate ? DateTime.Parse(pago.sFechaPagado) : DateTime.Now.ToLocalTime()
                                        };
                                        await _criptasRepositorio.DUpdateDisponible(entity);
                                    }
                                    
                                    await _emailService.EnviarCorreoPago(clienteResponse.Result, "LIQUIDADO", entidad.dMontoPagado.Value, cripta.Result.sNumero, pagoBD.iTipoPago);
                                    await _firebase.EnviarNotificacionAsync(
                                        clienteResponse.Result.sFcmToken,
                                        "Pago aplicado",
                                        $"Tu cripta ha sido liquidada con éxito. Monto {entidad.dMontoPagado:C} con Fecha de {DateTime.Now}"
                                    );
                                }
                            }else if (!response.HasError)
                            {
                                await _emailService.EnviarCorreoPago(clienteResponse.Result, "APLICADO", pago.dMontoPagado, cripta.Result.sNumero, pagoBD.iTipoPago);
                                await _firebase.EnviarNotificacionAsync(
                                        clienteResponse.Result.sFcmToken,
                                        "Pago aplicado",
                                        $"Tu cripta ha sido aplicado con éxito. Monto {pago.dMontoPagado:C} con Fecha de {DateTime.Now}"
                                    );
                            }
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
                    if (pagoBD.uIdCripta != new Guid(IdPermanentes.clienteGeneral.GetDescription()))
                    {
                        EntCriptas entity = new EntCriptas()
                        {
                            uId = pagoBD.uIdCripta,
                            bDisponible = true,
                            uIdCliente = new Guid(IdPermanentes.clienteGeneral.GetDescription())
                        };
                        await _criptasRepositorio.DUpdateBoolean(entity);
                    }
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

        public async Task<Response<EntPagos>> UpdateCriptaAfterBuy(EntCambioCripta pago)
        {
            var response = new Response<EntPagos>();
            try
            {
                var pagoBD = await _pagosRepositorio.DGetById(pago.uId);
                if (!pagoBD.HasError)
                {
                    var cripta = await _criptasRepositorio.DGetById(pagoBD.Result.uIdCripta);
                    if (!cripta.HasError)
                    {
                        pago.uIdCripta = cripta.Result.uId;
                        response = await _pagosRepositorio.DUpdateCriptaAfterBuy(pago);
                    }
                    else
                        response.SetError("Cripta no existe");
                }
                else
                    response = pagoBD;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del pago");
                
                response.SetError("Hubo un error al actualizar el estado del pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<bool>> DeletePagoById(Guid id)
        {
            try
            {
                var response = await _pagosRepositorio.DUpdateEliminado(id);
                return response;
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

        public async Task<Response<PagedResult<EntPagosLista>>> GetPagosByFilters(EntPagosSearchRequest filtros)
        {
            try
            {
                return await _pagosRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los pagos por filtros");
                var response = new Response<PagedResult<EntPagosLista>>();
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

        public async Task<Response<List<EntPagos>>> DGetByClienteId(Guid id, Guid uIdCripta)
        {
            try
            {
                return await _pagosRepositorio.DGetByClienteId(id, uIdCripta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el pago por ID de cliente");
                var response = new Response<List<EntPagos>>();
                response.SetError("Hubo un error al obtener el pago de cliente.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSolicitudPago>> SaveInfoPago(EntSolicitudPago entity)
        {
            var response = new Response<EntSolicitudPago>();
            try
            {
                var pago = await _pagosRepositorio.DGetInfoById(entity.uIdPago);
                if (!pago.HasError)
                {
                    response.SetError("Hay una evidencia pendiente");
                    return response;
                }
                entity.uId= Guid.NewGuid();
                var respuesta = await _pagosRepositorio.DSaveInfoPago(entity);
                return respuesta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el pago");
                response.SetError("Hubo un error al guardar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSolicitudPago>> UpdateInfoPago(EntSolicitudPago entity)
        {
            var response = new Response<EntSolicitudPago>();
            try {
                if (entity.bEstatus.Value)
                {
                    var pago = await _pagosRepositorio.DGetById(entity.uIdPago);
                    if(pago.HasError || pago.Result == null)
                    {
                        response.SetError("No existe el pago.");
                        response.HttpCode = HttpStatusCode.BadRequest;
                        return response;
                    }else if (pago.Result.bPagado)
                    {
                        response.SetError("El pago ya ha sido aplicado.");
                        response.HttpCode = HttpStatusCode.BadRequest;
                        return response;
                    }
                    else
                    {
                        ReadPagosRequest readPagosRequest = new ReadPagosRequest()
                        {
                            uIdPago = entity.uIdPago,
                            sFechaPagado = entity.dtFechaRegistro.ToString("yyyy-MM-dd"),
                            dMontoPagado = pago.Result.dMontoTotal,
                            bApplyDate = true
                        };
                        var respues = await UpdatePagado(readPagosRequest);
                        if (respues.HasError)
                        {
                            response.Message = respues.Message;
                            response.HttpCode = respues.HttpCode;
                            return response;
                        }
                    }
                }
                response = await _pagosRepositorio.DUpdateStatusInfoPago(entity);
                return response;
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el pago: "+ex.Message);
                response.SetError("Hubo un error al guardar el pago.");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
