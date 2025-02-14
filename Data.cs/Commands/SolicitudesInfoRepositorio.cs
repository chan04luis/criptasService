using AutoMapper;
using Data.cs.Entities.Catalogos;
using Data.cs.Interfaces.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Data.cs.Commands
{
    public class SolicitudesInfoRepositorio:ISolicitudesInfoRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SolicitudesInfoRepositorio> _logger;

        public SolicitudesInfoRepositorio(ApplicationDbContext dbContext, IMapper mapper, ILogger<SolicitudesInfoRepositorio> logger)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this._logger = logger;
        }

        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.SolicitudesInfo.AnyAsync(i => i.Id == pKey && !i.Eliminado);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Solicitud ya existente");
                }
                else
                {
                    response.SetError("No existe la solicitud");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> AnyExistSolicitud(Guid idCliente, Guid idServicio)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var existSolicitud = await dbContext.SolicitudesInfo
                    .AnyAsync(i => i.IdCliente == idCliente && i.IdServicio == idServicio && !i.Eliminado);

                if (existSolicitud)
                {
                    response.SetSuccess(existSolicitud, "Solicitud ya existente");
                }
                else
                {
                    response.SetError("No existe la solicitud");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistSolicitud));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntSolicitudesInfo>> DSave(EntSolicitudesInfo entity)
        {
            var response = new Response<EntSolicitudesInfo>();

            try
            {
                var newItem = _mapper.Map<SolicitudesInfo>(entity);
                newItem.Id = Guid.NewGuid();
                newItem.FechaRegistro = DateTime.Now.ToLocalTime();
                newItem.Eliminado = false;
                newItem.Atendido = false;
                newItem.IdCliente = entity.IdCliente;
                newItem.IdServicio = entity.IdServicio;
                newItem.Mensaje = entity.Mensaje;
                newItem.Visto = entity.Visto;

                dbContext.SolicitudesInfo.Add(newItem);
                int i = await dbContext.SaveChangesAsync();

                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar la solicitud");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntSolicitudesInfo>(newItem));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DSave));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntSolicitudesInfo>> DUpdate(EntSolicitudesInfo entity)
        {
            Response<EntSolicitudesInfo> response = new Response<EntSolicitudesInfo>();

            try
            {
                var bEntity = await dbContext.SolicitudesInfo.FindAsync(entity.Id);
                if (bEntity == null)
                {
                    response.SetError("La solicitud no fue encontrada.");
                    return response;
                }

                bEntity.IdCliente = entity.IdCliente;
                bEntity.IdServicio = entity.IdServicio;
                bEntity.Mensaje = entity.Mensaje;
                bEntity.Visto = entity.Visto;
                bEntity.Atendido = entity.Atendido;
                bEntity.FechaActualizacion = DateTime.Now.ToLocalTime();

                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntSolicitudesInfo>(bEntity), "Solicitud actualizada correctamente");
                else
                {
                    response.SetError("Registro no actualizado");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DUpdate));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DDelete(Guid uId)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var bEntity = await dbContext.SolicitudesInfo.FindAsync(uId);
                bEntity.Eliminado = true;
                bEntity.FechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Attach(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(true, "Solicitud eliminada correctamente");
                else
                {
                    response.SetError("Registro no eliminado");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DDelete));
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<EntSolicitudesInfo>> DGetById(Guid iKey)
        {
            var response = new Response<EntSolicitudesInfo>();

            try
            {
                var entity = await dbContext.SolicitudesInfo
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == iKey && !x.Eliminado);

                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntSolicitudesInfo>(entity));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DGetById));
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntSolicitudesInfo>>> DGetList()
        {
            var response = new Response<List<EntSolicitudesInfo>>();

            try
            {
                var items = await dbContext.SolicitudesInfo
                    .AsNoTracking()
                    .OrderBy(x => x.FechaRegistro)
                    .ToListAsync();

                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntSolicitudesInfo>>(items));
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DGetList));
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntSolicitudesInfo>>> DGetListActive()
        {
            var response = new Response<List<EntSolicitudesInfo>>();
            try
            {
                var items = await dbContext.SolicitudesInfo
                    .AsNoTracking()
                    .Where(x => !x.Eliminado)
                    .OrderBy(x => x.FechaRegistro)
                    .ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntSolicitudesInfo>>(items));
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DGetListActive));
                response.SetError(ex.Message);
            }
            return response;
        }


    }
}
