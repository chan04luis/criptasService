using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Commands.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.Extensions.Logging;
using Models.Request.Seguridad;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Seguridad
{
    public class BusPagina:IBusPagina
    {
        private readonly IPaginasRespositorio _datPagina;
        private readonly IMapper mapeador;
        private readonly ILogger<BusPagina> _logger;

        public BusPagina(IMapper mapeador, IPaginasRespositorio _datPagina, ILogger<BusPagina> _logger)
        {
            this.mapeador = mapeador;
            this._datPagina = _datPagina;
            this._logger = _logger;
        }
        public async Task<Response<PaginaModelo>> BCreate(PaginaRequest createModel)
        {
            Response<PaginaModelo> response = new Response<PaginaModelo>();

            try
            {
                Response<bool> existName = await _datPagina.AnyExitName(createModel.sNombrePagina);

                if (existName.Result)
                {
                    response.SetError(existName.Message);
                    return response;
                }


                Pagina paginaEntidad = mapeador.Map<Pagina>(createModel);

                var paginaCreada = await _datPagina.Save(paginaEntidad);

                if (paginaCreada.HasError)
                {
                    response.SetError(paginaCreada.Message);
                }
                else
                {
                    PaginaModelo entPaginaCreado = mapeador.Map<PaginaModelo>(paginaCreada.Result);
                    response.SetCreated(entPaginaCreado);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(BCreate));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> BDelete(Guid iKey)
        {
            Response<bool> response = new();
            try
            {
                var resData = await _datPagina.Delete(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(BDelete));
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<PaginaModelo>> BGet(Guid iKey)
        {
            Response<PaginaModelo> response = new Response<PaginaModelo>();
            try
            {
                var resData = await _datPagina.Get(iKey);

                PaginaModelo paginaModelo = mapeador.Map<PaginaModelo>(resData.Result);

                response.SetSuccess(paginaModelo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(BGet));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<List<PaginaModelo>>> BGetAll()
        {
            Response<List<PaginaModelo>> response = new Response<List<PaginaModelo>>();
            try
            {
                var resData = await _datPagina.GetAll();
                var listadoMapeador = mapeador.Map<List<PaginaModelo>>(resData.Result);
                response.SetSuccess(listadoMapeador);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(BGetAll));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> BUpdate(PaginaRequest updateModel)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                Pagina entPaginal = mapeador.Map<Pagina>(updateModel);
                var paginaExistente = await _datPagina.AnyExitNameAndKey(entPaginal);
                if (paginaExistente.Result)
                {
                    response.SetError("Pagina ya existente.");
                    return response;
                }

                var result = await _datPagina.Update(entPaginal);

                if (result.Result)
                {

                    response.SetSuccess(result.Result, result.Message);
                }
                else
                {
                    response.SetError(result.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(BUpdate));
                response.SetError(ex);
            }

            return response;
        }
    }
}
