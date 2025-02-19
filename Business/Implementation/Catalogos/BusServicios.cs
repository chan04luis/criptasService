using AutoMapper;
using Business.Interfaces.Catalogos;
using Data.cs.Interfaces.Catalogos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Responses.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Catalogos
{
    public class BusServicios:IBusServicios
    {
        private readonly IServiciosRepositorio _serviciosRepositorio;
        private readonly ILogger<BusServicios> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BusServicios(IServiciosRepositorio serviciosRepositorio, ILogger<BusServicios> logger, IMapper mapper, IConfiguration configuration)
        {
            _serviciosRepositorio = serviciosRepositorio;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Response<EntServicios>> SaveServicio(EntServicios servicio)
        {
            var response = new Response<EntServicios>();

            try
            {
                var existName = await _serviciosRepositorio.DAnyExistName(servicio.Nombre);
                if (existName.Result)
                {
                    response.SetError(existName.Message);
                    return response;
                }

                var servicioCreado = await _serviciosRepositorio.DSave(servicio);

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
                var result = await _serviciosRepositorio.DUpdate(entServicio);

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
                return await _serviciosRepositorio.DUpdateEstatus(entServicio);
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
                var existKey = await _serviciosRepositorio.AnyExistKey(id);
                if (!existKey.Result)
                {
                    response.SetError(existKey.Message);
                    return response;
                }

                response = await _serviciosRepositorio.DDelete(id);
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
                return await _serviciosRepositorio.DGetById(id);
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
                return await _serviciosRepositorio.DGetList();
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
                return await _serviciosRepositorio.DGetListActive();
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

        public async Task<Response<List<EntServiceItem>>> GetListActive(Guid uIdIglesia)
        {
            try
            {
                return await _serviciosRepositorio.DGetListActive(uIdIglesia);
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
    }
}
