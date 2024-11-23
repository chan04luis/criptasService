using AutoMapper;
using Business.Data;
using Business.Interfaces;
using Entities.Models;
using Entities.Request.Secciones;
using Entities;
using Microsoft.Extensions.Logging;
using Utils.Interfaces;

namespace Business.Implementation
{
    public class BusSecciones : IBusSecciones
    {
        private readonly ISeccionesRepositorio _seccionesRepositorio;
        private readonly IFiltros _filtros;
        private readonly ILogger<BusSecciones> _logger;
        private readonly IMapper _mapper;

        public BusSecciones(ISeccionesRepositorio seccionesRepositorio, IFiltros filtros, ILogger<BusSecciones> logger, IMapper mapper)
        {
            _seccionesRepositorio = seccionesRepositorio;
            _filtros = filtros;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<EntSecciones>> ValidateAndSaveSection(EntSeccionRequest seccion)
        {
            var response = new Response<EntSecciones>();

            try
            {
                if (string.IsNullOrWhiteSpace(seccion.sNombre))
                {
                    response.SetError("El campo Nombre es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _seccionesRepositorio.DGetByName(seccion.sNombre, seccion.uIdZona);
                if (!item.HasError && item.Result.Count > 0)
                {
                    response.SetError("Sección ya registrada con ese nombre.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                EntSecciones nuevaSeccion = new EntSecciones
                {
                    uId = Guid.NewGuid(),
                    uIdZona = seccion.uIdZona,
                    sNombre = seccion.sNombre,
                    bEstatus = true,
                    dtFechaRegistro = DateTime.Now.ToLocalTime(),
                    dtFechaActualizacion = DateTime.Now.ToLocalTime()
                };

                return await SaveSection(nuevaSeccion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y guardar la sección");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSecciones>> SaveSection(EntSecciones seccion)
        {
            try
            {
                return await _seccionesRepositorio.DSave(_mapper.Map<EntSecciones>(seccion));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la sección");
                var response = new Response<EntSecciones>();
                response.SetError("Hubo un error al guardar la sección.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSecciones>> ValidateAndUpdateSection(EntSeccionesUpdateRequest seccion)
        {
            var response = new Response<EntSecciones>();

            try
            {
                if (string.IsNullOrWhiteSpace(seccion.sNombre))
                {
                    response.SetError("El campo Nombre es obligatorio.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var item = await _seccionesRepositorio.DGetByName(seccion.sNombre, seccion.uIdZona);
                if (!item.HasError && item.Result.Where(x => x.uId != seccion.uId).ToList().Count > 0)
                {
                    response.SetError("Sección ya registrada con ese nombre.");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var seccionActualizada = _mapper.Map<EntSecciones>(seccion);
                return await UpdateSection(seccionActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar y actualizar la sección");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSecciones>> UpdateSection(EntSecciones seccion)
        {
            try
            {
                return await _seccionesRepositorio.DUpdate(seccion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la sección");
                var response = new Response<EntSecciones>();
                response.SetError("Hubo un error al actualizar la sección.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSecciones>> UpdateSectionStatus(EntSeccionesUpdateEstatusRequest seccion)
        {
            try
            {
                EntSecciones seccionActualizada = new EntSecciones
                {
                    uId = seccion.uId,
                    bEstatus = seccion.bEstatus
                };
                return await _seccionesRepositorio.DUpdateBoolean(seccionActualizada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado de la sección");
                var response = new Response<EntSecciones>();
                response.SetError("Hubo un error al actualizar el estado de la sección.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<bool>> DeleteSectionById(Guid id)
        {
            try
            {
                return await _seccionesRepositorio.DUpdateEliminado(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la sección por ID");
                var response = new Response<bool>();
                response.SetError("Hubo un error al eliminar la sección.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<EntSecciones>> GetSectionById(Guid id)
        {
            try
            {
                return await _seccionesRepositorio.DGetById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la sección por ID");
                var response = new Response<EntSecciones>();
                response.SetError("Hubo un error al obtener la sección.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntSecciones>>> GetSectionsByFilters(EntSeccionSearchRequest filtros)
        {
            try
            {
                return await _seccionesRepositorio.DGetByFilters(filtros);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las secciones por filtros");
                var response = new Response<List<EntSecciones>>();
                response.SetError("Hubo un error al obtener las secciones.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        public async Task<Response<List<EntSecciones>>> GetSectionList(Guid uIdIglesia)
        {
            try
            {
                return await _seccionesRepositorio.DGetList(uIdIglesia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de secciones");
                var response = new Response<List<EntSecciones>>();
                response.SetError("Hubo un error al obtener la lista de secciones.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }

}
