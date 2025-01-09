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
    public class BusBoton:IBusBoton
    {
        private readonly IBotonesRepositorio botonesRepositorio;
        private readonly IMapper mapeador;
        private readonly ILogger<BusBoton> _logger;

        public BusBoton(IMapper mapeador, IBotonesRepositorio botonesRepositorio, ILogger<BusBoton> _logger)
        {
            this.mapeador = mapeador;
            this.botonesRepositorio = botonesRepositorio;
            this._logger = _logger;
        }
        public async Task<Response<BotonModelo>> BCreate(BotonRequest createModel)
        {
            Response<BotonModelo> response = new Response<BotonModelo>();

            try
            {
                Response<bool> existName = await botonesRepositorio.AnyExitName(createModel.sClaveBoton);

                if (existName.Result)
                {
                    response.SetError(existName.Message);
                    return response;
                }


                Boton botonEntidad = mapeador.Map<Boton>(createModel);

                var botonCreado = await botonesRepositorio.Save(botonEntidad);

                if (botonCreado.HasError)
                {
                    response.SetError(botonCreado.Message);
                }
                else
                {
                    BotonModelo entBotonCreado = mapeador.Map<BotonModelo>(botonCreado.Result);
                    response.SetCreated(entBotonCreado);
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
                var existKey = await botonesRepositorio.AnyExistKey(iKey);
                if (!existKey.Result)
                {
                    response.SetError(existKey.Message);
                    return response;
                }
                var resData = await botonesRepositorio.Delete(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(BDelete));
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<bool>> BUpdate(BotonRequest updateModel)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                Boton entBoton = mapeador.Map<Boton>(updateModel);
                var paginaExistente = await botonesRepositorio.AnyExitNameAndKey(entBoton);
                if (paginaExistente.Result)
                {
                    response.SetError("Boton ya existente.");
                    return response;
                }

                var result = await botonesRepositorio.Update(entBoton);

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
