using AutoMapper;
using Microsoft.Extensions.Logging;
using Business.Interfaces.Catalogos;
using Models.Models;
using Models.Request.Visitas;
using Utils;
using Business.Data;
using Utils.Implementation;

namespace Business.Implementation.Catalogos
{
    public class BusVisitas : IBusVisitas
    {
        private readonly IVisitasRepositorio _visitasRepositorio;
        private readonly IFallecidosRepositorio _fallecido;
        private readonly ICriptasRepositorio _cripta;
        private readonly IClientesRepositorio _cliente;
        private readonly ILogger<BusVisitas> _logger;
        private readonly IMapper _mapper;
        private readonly FirebaseNotificationService _firebase;
        private readonly EmailService _emailService;

        public BusVisitas(IVisitasRepositorio visitasRepositorio, ILogger<BusVisitas> logger, IMapper mapper, IFallecidosRepositorio fallecidos,
            ICriptasRepositorio cripta, IClientesRepositorio cliente, FirebaseNotificationService firebase, EmailService emailService)
        {
            _visitasRepositorio = visitasRepositorio;
            _logger = logger;
            _mapper = mapper;
            _fallecido = fallecidos;
            _cripta = cripta;
            _cliente = cliente;
            _emailService = emailService;
            _firebase = firebase;
        }

        public async Task<Response<EntVisitas>> SaveVisit(EntVisitasRequest visita)
        {
            var response = new Response<EntVisitas>();

            try
            {
                EntVisitas entVisita = new EntVisitas
                {
                    uId = Guid.NewGuid(),
                    uIdCriptas = visita.uIdCriptas,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    sNombreVisitante = visita.sNombreVisitante,
                    sMensaje = visita.sMensaje
                };

                var resp =  await _visitasRepositorio.DSave(entVisita);
                var fallecido = await _fallecido.DGetById(visita.uIdCriptas);
                if(!fallecido.HasError && fallecido.Result != null)
                {
                    var cripta = await _cripta.DGetById(fallecido.Result.uIdCripta);
                    if (!cripta.HasError && cripta.Result != null)
                    {
                        var client = await _cliente.DGetById(cripta.Result.uIdCliente);
                        if (!client.HasError && client.Result != null)
                        {
                            await _emailService.EnviarCorreoVisita(
                                client.Result.sEmail,
                                visita.sNombreVisitante,
                                fallecido.Result.sNombre + " " + fallecido.Result.sApellidos,
                                entVisita.dtFechaRegistro,
                                visita.sMensaje
                            );
                            await _firebase.EnviarNotificacionAsync(
                                client.Result.sFcmToken,
                                "Nueva Visita Registrada",
                                $"{visita.sNombreVisitante} ha visitado la cripta de {fallecido.Result.sNombre} {fallecido.Result.sApellidos} el {entVisita.dtFechaRegistro:dd/MM/yyyy HH:mm}. Mensaje: \"{visita.sMensaje}\"."
                            );

                        }
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la visita");
                response.SetError("Hubo un error al guardar la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntVisitas>> UpdateVisit(EntVisitasRequest visita)
        {
            var response = new Response<EntVisitas>();

            try
            {
                var updatedVisit = _mapper.Map<EntVisitas>(visita);
                return await _visitasRepositorio.DUpdate(updatedVisit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la visita");
                response.SetError("Hubo un error al actualizar la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntVisitas>> GetVisitById(Guid uId)
        {
            try
            {
                return await _visitasRepositorio.DGetById(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la visita por ID");
                var response = new Response<EntVisitas>();
                response.SetError("Hubo un error al obtener la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntVisitas>>> GetVisitsByFilters(EntVisitasSearchRequest filters)
        {
            try
            {
                return await _visitasRepositorio.DGetByFilters(filters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las visitas por filtros");
                var response = new Response<List<EntVisitas>>();
                response.SetError("Hubo un error al obtener las visitas.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteVisit(Guid uId)
        {
            try
            {
                return await _visitasRepositorio.DUpdateEliminado(uId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la visita");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar la visita.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }
}
