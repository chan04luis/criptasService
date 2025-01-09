using AutoMapper;
using Business.Interfaces.Seguridad;
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
    public class BusModulo:IBusModulo
    {
        private readonly IModulosRepositorio _datModulo;
        private readonly IMapper mapeador;
        private readonly ILogger<BusModulo> _logger;
        public BusModulo(IMapper mapeador, IModulosRepositorio _datModulo, ILogger<BusModulo> _logger)
        {
            this.mapeador = mapeador;
            this._datModulo = _datModulo;
            this._logger = _logger;
        }

        public async Task<Response<ModuloModelo>> BCreate(ModuloRequest createModel)
        {
            Response<ModuloModelo> response = new Response<ModuloModelo>();

            try
            {
                Response<bool> existName = await _datModulo.AnyExitName(createModel.sNombreModulo);

                if (existName.Result)
                {
                    response.SetError(existName.Message);
                    return response;
                }


                Modulo moduloEntidad = mapeador.Map<Modulo>(createModel);

                var moduloCreada = await _datModulo.Save(moduloEntidad);

                if (moduloCreada.HasError)
                {
                    response.SetError(moduloCreada.Message);
                }
                else
                {
                    ModuloModelo entModuloCreado = mapeador.Map<ModuloModelo>(moduloCreada.Result);
                    response.SetCreated(entModuloCreado);
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
                var existKey = await _datModulo.AnyExistKey(iKey);
                if (!existKey.Result)
                {
                    response.SetError(existKey.Message);
                    return response;
                }
                var resData = await _datModulo.Delete(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(BDelete));
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<bool>> BUpdate(ModuloRequest updateModel)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                Modulo entModulo = mapeador.Map<Modulo>(updateModel);
                var moduloExistente = await _datModulo.AnyExitNameAndKey(entModulo);
                if (moduloExistente.Result)
                {
                    response.SetError("Modulo ya existente.");
                    return response;
                }

                var result = await _datModulo.Update(entModulo);

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
