

using AutoMapper;
using Business.Data;
using Business.Interfaces;
using Entities.Models;
using Entities.Request.Criptas;
using Entities;
using Microsoft.Extensions.Logging;
using Utils.Interfaces;

namespace Business.Implementation
{
    public class BusCriptas : IBusCriptas
    {
        private readonly ICriptasRepositorio _criptasRepositorio;
        private readonly IFiltros _filtros;
        private readonly ILogger<BusCriptas> _logger;
        private readonly IMapper _mapper;

        public BusCriptas(ICriptasRepositorio criptasRepositorio, IFiltros filtros, ILogger<BusCriptas> logger, IMapper mapper)
        {
            _criptasRepositorio = criptasRepositorio;
            _filtros = filtros;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntCriptas>> ValidateAndSaveCripta(EntCriptaRequest cripta)
        {
            var response = new Response<EntCriptas>();

            try
            {
                if (string.IsNullOrWhiteSpace(cripta.sNumero))
                {
                    response.SetError("El campo Número es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _criptasRepositorio.DGetByName(cripta.sNumero, cripta.uIdSeccion);
                if (!item.HasError && item.Result.Count > 0)
                {
                    response.SetError("Cripta ya registrada con ese número.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                EntCriptas nuevaCripta = new EntCriptas
                {
                    uId = Guid.NewGuid(),
                    uIdCliente = cripta.uIdCliente,
                    uIdSeccion = cripta.uIdSeccion,
                    sNumero = cripta.sNumero,
                    sUbicacionEspecifica = cripta.sUbicacionEspecifica,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                return await SaveCripta(nuevaCripta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar la cripta");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntCriptas>> SaveCripta(EntCriptas cripta)
        {
            try
            {
                return await _criptasRepositorio.DSave(_mapper.Map<EntCriptas>(cripta));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la cripta");
                var response = new Response<EntCriptas>();
                response.SetError("Hubo un error al guardar la cripta.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntCriptas>> ValidateAndUpdateCripta(EntCriptaUpdateRequest cripta)
        {
            var response = new Response<EntCriptas>();

            try
            {
                if (string.IsNullOrWhiteSpace(cripta.sNumero))
                {
                    response.SetError("El campo Número es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _criptasRepositorio.DGetByName(cripta.sNumero, cripta.uIdSeccion);
                if (!item.HasError && item.Result.Where(x => x.uId != cripta.uId).ToList().Count > 0)
                {
                    response.SetError("Cripta ya registrada con ese número.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var criptaActualizada = _mapper.Map<EntCriptas>(cripta);
                return await UpdateCripta(criptaActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar la cripta");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntCriptas>> UpdateCripta(EntCriptas cripta)
        {
            try
            {
                return await _criptasRepositorio.DUpdate(cripta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la cripta");
                var response = new Response<EntCriptas>();
                response.SetError("Hubo un error al actualizar la cripta.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntCriptas>> UpdateCriptaStatus(EntCriptaUpdateEstatusRequest cripta)
        {
            try
            {
                EntCriptas criptaActualizada = new EntCriptas
                {
                    uId = cripta.uId,
                    bEstatus = cripta.bEstatus
                };
                return await _criptasRepositorio.DUpdateBoolean(criptaActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado de la cripta");
                var response = new Response<EntCriptas>();
                response.SetError("Hubo un error al actualizar el estado de la cripta.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteCriptaById(Guid id)
        {
            try
            {
                return await _criptasRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la cripta por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar la cripta.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntCriptas>> GetCriptaById(Guid id)
        {
            try
            {
                return await _criptasRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la cripta por ID");
                var response = new Response<EntCriptas>();
                response.SetError("Hubo un error al obtener la cripta.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntCriptas>>> GetCriptasByFilters(EntCriptaSearchRequest filtros)
        {
            try
            {
                return await _criptasRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las criptas por filtros");
                var response = new Response<List<EntCriptas>>();
                response.SetError("Hubo un error al obtener las criptas.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntCriptas>>> GetCriptaList(Guid uIdSeccion)
        {
            try
            {
                return await _criptasRepositorio.DGetList(uIdSeccion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de criptas");
                var response = new Response<List<EntCriptas>>();
                response.SetError("Hubo un error al obtener la lista de criptas.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
