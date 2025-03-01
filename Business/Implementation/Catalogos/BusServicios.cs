using AutoMapper;
using Business.Interfaces.Catalogos;
using Data.cs.Interfaces.Catalogos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Responses.Servicio;
using Utils;

namespace Business.Implementation.Catalogos
{
    public class BusServicios:IBusServicios
    {
        private readonly IServiciosRepositorio _repositorio;
        private readonly ILogger<BusServicios> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BusServicios(IServiciosRepositorio serviciosRepositorio, ILogger<BusServicios> logger, IMapper mapper, IConfiguration configuration)
        {
            _repositorio = serviciosRepositorio;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Response<EntServicios>> SaveServicio(EntServicios servicio)
        {
            var response = new Response<EntServicios>();

            try
            {
                var existName = await _repositorio.DAnyExistName(servicio.Nombre);
                if (existName.Result)
                {
                    response.SetError(existName.Message);
                    return response;
                }

                var servicioCreado = await _repositorio.DSave(servicio);

                response.SetSuccess(servicioCreado.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el servicio");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<EntServicios>> UpdateServicio(EntServicios entServicio)
        {
            var response = new Response<EntServicios>();

            try
            {
                var result = await _repositorio.DUpdate(entServicio);

                response.SetSuccess(result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el servicio");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<EntServicios>> UpdateServicioStatus(EntServicios entServicio)
        {
            try
            {
                return await _repositorio.DUpdateEstatus(entServicio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del servicio");
                var response = new Response<EntServicios>();
                response.SetError("Hubo un error al actualizar el estado del servicio.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteServicioById(Guid id)
        {
            var response = new Response<bool>();

            try
            {
                var existKey = await _repositorio.AnyExistKey(id);
                if (!existKey.Result)
                {
                    response.SetError(existKey.Message);
                    return response;
                }

                response = await _repositorio.DDelete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el servicio");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<EntServicios>> BGetById(Guid id)
        {
            try
            {
                return await _repositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del servicio");
                var response = new Response<EntServicios>();
                response.SetError("Hubo un error al actualizar el estado del servicio.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntServicios>>> GetServicioList()
        {
            try
            {
                return await _repositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de servicios");
                var response = new Response<List<EntServicios>>();
                response.SetError("Hubo un error al obtener la lista de servicios.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntServicios>>> GetListActive()
        {
            try
            {
                return await _repositorio.DGetListActive();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de servicios activos");
                var response = new Response<List<EntServicios>>();
                response.SetError("Hubo un error al obtener la lista de servicios activos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntServiceItem>>> GetListActive(Guid uId)
        {
            try
            {
                return await _repositorio.DGetListActive(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de servicios activos");
                var response = new Response<List<EntServiceItem>>();
                response.SetError("Hubo un error al obtener la lista de servicios activos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntServiceItem>>> BGetListPreAssigment(Guid uId)
        {
            try
            {
                return await _repositorio.DGetListPreAssigment(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de servicios activos");
                var response = new Response<List<EntServiceItem>>();
                response.SetError("Hubo un error al obtener la lista de servicios activos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> BSaveToSucursal(List<EntServiceItem> entities, Guid uId)
        {
            try
            {
                return await _repositorio.DSaveToSucursal(entities, uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la lista de servicios activos");
                var response = new Response<bool>();
                response.SetError("Hubo un error al actualizar la lista de servicios activos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntServiceItem>>> BGetListPreAssigmentUser(Guid uId)
        {
            try
            {
                return await _repositorio.DGetListPreAssigmentUser(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de servicios activos");
                var response = new Response<List<EntServiceItem>>();
                response.SetError("Hubo un error al obtener la lista de servicios activos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> BSaveToUser(List<EntServiceItem> entities, Guid uId)
        {
            try
            {
                return await _repositorio.DSaveToUser(entities, uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la lista de servicios activos");
                var response = new Response<bool>();
                response.SetError("Hubo un error al actualizar la lista de servicios activos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
