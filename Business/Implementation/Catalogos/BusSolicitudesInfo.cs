using AutoMapper;
using Business.Interfaces.Catalogos;
using Data.cs.Interfaces.Catalogos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Models;
using Utils;

namespace Business.Implementation.Catalogos
{
    public class BusSolicitudesInfo:IBusSolicitudesInfo
    {
        private readonly ISolicitudesInfoRepositorio _solicitudesInfoRepositorio;
        private readonly ILogger<BusSolicitudesInfo> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BusSolicitudesInfo(
            ISolicitudesInfoRepositorio solicitudesInfoRepositorio,
            ILogger<BusSolicitudesInfo> logger,
            IMapper mapper,
            IConfiguration configuration)
        {
            _solicitudesInfoRepositorio = solicitudesInfoRepositorio;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Response<EntSolicitudesInfo>> SaveSolicitud(EntSolicitudesInfo solicitud)
        {
            var response = new Response<EntSolicitudesInfo>();

            try
            {
                var existSolicitud = await _solicitudesInfoRepositorio.AnyExistSolicitud(solicitud.IdCliente, solicitud.IdServicio);
                if (existSolicitud.Result)
                {
                    response.SetError(existSolicitud.Message);
                    return response;
                }

                var solicitudCreada = await _solicitudesInfoRepositorio.DSave(solicitud);

                response.SetSuccess(solicitudCreada.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la solicitud");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<EntSolicitudesInfo>> UpdateSolicitud(EntSolicitudesInfo entSolicitud)
        {
            var response = new Response<EntSolicitudesInfo>();

            try
            {
                var result = await _solicitudesInfoRepositorio.DUpdate(entSolicitud);

                response.SetSuccess(result.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la solicitud");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<bool>> DeleteSolicitudById(Guid id)
        {
            var response = new Response<bool>();

            try
            {
                var existKey = await _solicitudesInfoRepositorio.AnyExistKey(id);
                if (!existKey.Result)
                {
                    response.SetError(existKey.Message);
                    return response;
                }

                response = await _solicitudesInfoRepositorio.DDelete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la solicitud");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<EntSolicitudesInfo>> GetSolicitudById(Guid id)
        {
            var response = new Response<EntSolicitudesInfo>();

            try
            {
                var result = await _solicitudesInfoRepositorio.DGetById(id);

                if (result.HasError)
                {
                    response.SetError(result.Message);
                    response.HttpCode = result.HttpCode;
                }
                else
                {
                    response.SetSuccess(result.Result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la solicitud por ID");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public async Task<Response<List<EntSolicitudesInfo>>> GetSolicitudList()
        {
            try
            {
                return await _solicitudesInfoRepositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de solicitudes");
                var response = new Response<List<EntSolicitudesInfo>>();
                response.SetError("Hubo un error al obtener la lista de solicitudes.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
        public async Task<Response<List<EntSolicitudesInfo>>> GetListActive()
        {
            try
            {
                return await _solicitudesInfoRepositorio.DGetListActive();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de Solicitudes activos");
                var response = new Response<List<EntSolicitudesInfo>>();
                response.SetError("Hubo un error al obtener la lista de Solicitudes activos.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
