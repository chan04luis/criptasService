using AutoMapper;
using Business.Data;
using Business.Implementation.Seguridad;
using Business.Interfaces.Catalogos;
using Data.cs.Entities.Catalogos;
using Data.cs.Interfaces.Catalogos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Request.Usuarios;
using Models.Validations.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Implementation;
using Utils.Interfaces;

namespace Business.Implementation.Catalogos
{
    public class BusTipoDeMantenimiento:IBusTipoDeMantenimiento
    {
        private readonly ITipoDeMantenimientoRepositorio tipoDeMantenimientoRepositorio;
        private readonly ILogger<BusTipoDeMantenimiento> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BusTipoDeMantenimiento(ITipoDeMantenimientoRepositorio tipoDeMantenimientoRepositorio, ILogger<BusTipoDeMantenimiento> logger, IMapper mapper, IConfiguration configuration)
        {
            this.tipoDeMantenimientoRepositorio = tipoDeMantenimientoRepositorio;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<Response<EntTipoDeMantenimiento>> SaveTipoDeMantenimiento(EntTipoDeMantenimiento tipoDeMantenimiento)
        {
            var response = new Response<EntTipoDeMantenimiento>();

            try
            {

                var existName = await tipoDeMantenimientoRepositorio.DAnyExistName(tipoDeMantenimiento.Nombre);
                if (existName.Result)
                {
                    response.SetError(existName.Message);
                    return response;
                }

                var tipoDeMantenimientoMapeado = _mapper.Map<TipoDeMantenimiento>(tipoDeMantenimiento);

                var usuarioCreado = await tipoDeMantenimientoRepositorio.DSave(tipoDeMantenimientoMapeado);

                response.SetSuccess(usuarioCreado.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar el tipo de mantenimiento");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }
        public async Task<Response<EntTipoDeMantenimiento>> UpdateTipoDeMantenimiento(EntTipoDeMantenimiento entTipoDeMantenimiento)
        {
            var response = new Response<EntTipoDeMantenimiento>();

            try
            {

                var result = await tipoDeMantenimientoRepositorio.DUpdate(entTipoDeMantenimiento);

                response.SetSuccess(result.Result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el tipo de mantenimiento");
                response.SetError("Hubo un error al procesar la solicitud.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }
            return response;
        }
        public async Task<Response<EntTipoDeMantenimiento>> UpdateTipoDeMantenimientoStatus(EntTipoDeMantenimiento entTipoDeMantenimiento)
        {
            try
            {
                return await tipoDeMantenimientoRepositorio.DUpdateEstatus(entTipoDeMantenimiento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el estado del tipo de mantenimiento");
                var response = new Response<EntUsuarios>();
                response.SetError("Hubo un error al actualizar el estado del usuario.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
        public async Task<Response<bool>> DeleteTipoDeMantenimientoById(Guid id)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var existKey = await tipoDeMantenimientoRepositorio.AnyExistKey(id);
                if (!existKey.Result)
                {
                    response.SetError(existKey.Message);
                    return response;
                }

                response = await tipoDeMantenimientoRepositorio.DDelete(id);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<List<EntTipoDeMantenimiento>>> GetTipoDeMantenimientoList()
        {
            try
            {
                return await tipoDeMantenimientoRepositorio.DGetList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de tipo de mantenimiento");
                var response = new Response<List<EntTipoDeMantenimiento>>();
                response.SetError("Hubo un error al obtener la lista de tipos de mantenimiento.");
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }

    }
}
