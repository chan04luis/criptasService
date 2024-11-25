using AutoMapper;
using Business.Data;
using Business.Interfaces;
using Entities.Models;
using Entities.Request.TipoPagos;
using Entities;
using Microsoft.Extensions.Logging;
using Utils.Implementation;

namespace Business.Implementation
{
    public class BusTiposPago : IBusTiposPago
    {
        private readonly ITiposPagoRepositorio _tiposPagoRepositorio;
        private readonly ILogger<BusTiposPago> _logger;
        private readonly IMapper _mapper;

        public BusTiposPago(ITiposPagoRepositorio tiposPagoRepositorio, ILogger<BusTiposPago> logger, IMapper mapper)
        {
            _tiposPagoRepositorio = tiposPagoRepositorio;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntTiposPago>> ValidateAndSaveTipoPago(EntTiposPagoRequest tipoPago)
        {
            var response = new Response<EntTiposPago>();

            try
            {
                if (string.IsNullOrWhiteSpace(tipoPago.sNombre))
                {
                    response.SetError("El campo Nombre es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var existing = await _tiposPagoRepositorio.GetByName(tipoPago.sNombre);
                if (!existing.HasError && existing.Result.Any())
                {
                    response.SetError("Ya existe un tipo de pago con ese nombre.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var newTipoPago = new EntTiposPago
                {
                    uId = Guid.NewGuid(),
                    sNombre = tipoPago.sNombre,
                    sDescripcion = tipoPago.sDescripcion,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                };

                return await _tiposPagoRepositorio.Save(newTipoPago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar el tipo de pago");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntTiposPago>> ValidateAndUpdateTipoPago(Guid uId, EntTiposPagoRequest tipoPago)
        {
            var response = new Response<EntTiposPago>();

            try
            {
                if (string.IsNullOrWhiteSpace(tipoPago.sNombre))
                {
                    response.SetError("El campo Nombre es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var existing = await _tiposPagoRepositorio.GetByName(tipoPago.sNombre);
                if (!existing.HasError && existing.Result.Any(x => x.uId != uId))
                {
                    response.SetError("Ya existe un tipo de pago con ese nombre.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var updatedTipoPago = new EntTiposPago
                {
                    uId = uId,
                    sNombre = tipoPago.sNombre,
                    sDescripcion = tipoPago.sDescripcion,
                    bEstatus = true,
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                return await _tiposPagoRepositorio.Update(updatedTipoPago);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar el tipo de pago");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntTiposPago>> UpdateTipoPagoStatus(Guid uId, bool? bEstatus)
        {
            try
            {
                return await _tiposPagoRepositorio.UpdateEstatus(uId, bEstatus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del tipo de pago");
                var response = new Response<EntTiposPago>();
                response.SetError("Hubo un error al actualizar el estado del tipo de pago.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteTipoPagoById(Guid uId)
        {
            try
            {
                return await _tiposPagoRepositorio.UpdateEliminado(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el tipo de pago por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar el tipo de pago.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntTiposPago>> GetTipoPagoById(Guid uId)
        {
            try
            {
                return await _tiposPagoRepositorio.GetById(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el tipo de pago por ID");
                var response = new Response<EntTiposPago>();
                response.SetError("Hubo un error al obtener el tipo de pago.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntTiposPago>>> GetTiposPagoByFilters(EntTiposPagoSearchRequest filtros)
        {
            try
            {
                return await _tiposPagoRepositorio.GetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de pago por filtros");
                var response = new Response<List<EntTiposPago>>();
                response.SetError("Hubo un error al obtener los tipos de pago.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntTiposPago>>> GetList()
        {
            try
            {
                return await _tiposPagoRepositorio.GetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de pago por filtros");
                var response = new Response<List<EntTiposPago>>();
                response.SetError("Hubo un error al obtener los tipos de pago.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
