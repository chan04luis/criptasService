using AutoMapper;
using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Seguridad;
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
    public class TipoDeMantenimientoRepositorio:ITipoDeMantenimientoRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TipoDeMantenimientoRepositorio> _logger;

        public TipoDeMantenimientoRepositorio(ApplicationDbContext dbContext, IMapper mapper, ILogger<TipoDeMantenimientoRepositorio> _logger)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            this._logger = _logger;
        }

        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.TipoDeMantenimiento.AnyAsync(i => i.Id == pKey && !i.bEliminado);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Tipo de mantenimiento ya existente");
                }
                else
                {
                    response.SetError("No existe el Tipo de mantenimiento");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitMailAndKey(EntTipoDeMantenimiento pEntity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var existMail = await dbContext.TipoDeMantenimiento.AnyAsync(i => (i.Id != pEntity.Id) && (i.Nombre.Equals(pEntity.Nombre)));

                if (existMail)
                {
                    response.SetSuccess(existMail, "Tipo de mantenimiento ya existente");
                }
                else
                {
                    response.SetError("No existe el tipo de mantenimiento");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExitMailAndKey));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> DAnyExistName(string nombre)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var existMail = await dbContext.TipoDeMantenimiento.AnyAsync(i => i.Nombre.Equals(nombre) && !i.bEliminado);

                if (existMail)
                {
                    response.SetSuccess(existMail, "Tipo de mantenimiento ya existente");
                }
                else
                {
                    response.SetError("No existe el tipo de mantenimiento");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DAnyExistName));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<EntTipoDeMantenimiento>> DSave(EntTipoDeMantenimiento entity)
        {
            var response = new Response<EntTipoDeMantenimiento>();
            try
            {
                var newItem = _mapper.Map<TipoDeMantenimiento>(entity);
                newItem.Id = Guid.NewGuid();
                newItem.Nombre = entity.Nombre;
                newItem.Descripcion = entity.Descripcion;
                newItem.Costo = entity.Costo;
                newItem.Estatus = true;
                newItem.Img = entity.Img;
                newItem.bEliminado = false;
                newItem.FechaRegistro = DateTime.Now.ToLocalTime();
                dbContext.TipoDeMantenimiento.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntTipoDeMantenimiento>(newItem));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DSave));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<EntTipoDeMantenimiento>> DUpdate(EntTipoDeMantenimiento entity)
        {
            Response<EntTipoDeMantenimiento> response = new Response<EntTipoDeMantenimiento>();

            try
            {
                var bEntity = await dbContext.TipoDeMantenimiento.FindAsync(entity.Id);
                if (bEntity == null)
                {
                    response.SetError("El tipo de mantenimiento no fue encontrado.");
                    return response;
                }
                bEntity.Nombre = entity.Nombre;
                bEntity.Descripcion = entity.Descripcion;
                bEntity.Estatus = entity.Estatus;
                bEntity.Costo = entity.Costo;
                bEntity.Img = entity.Img;
                bEntity.FechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntTipoDeMantenimiento>(bEntity), "Actualizado correctamente");
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
        public async Task<Response<EntTipoDeMantenimiento>> DUpdateEstatus(EntTipoDeMantenimiento entity)
        {
            Response<EntTipoDeMantenimiento> response = new Response<EntTipoDeMantenimiento>();

            try
            {
                var bEntity = await dbContext.TipoDeMantenimiento.FindAsync(entity.Id);
                bEntity.Estatus = entity.Estatus;
                bEntity.FechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Attach(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntTipoDeMantenimiento>(bEntity), "Actualizado correctamente");
                else
                {
                    response.SetError("Registro no actualizado");
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DUpdateEstatus));
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> DDelete(Guid uId)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var bEntity = await dbContext.TipoDeMantenimiento.FindAsync(uId);
                bEntity.bEliminado = true;
                bEntity.FechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Attach(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(true, "Eliminado correctamente");
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
        public async Task<Response<EntTipoDeMantenimiento>> DGetById(Guid iKey)
        {
            var response = new Response<EntTipoDeMantenimiento>();
            try
            {
                var entity = await dbContext.TipoDeMantenimiento.AsNoTracking().FirstOrDefaultAsync(x => x.Id == iKey && !x.bEliminado);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntTipoDeMantenimiento>(entity));
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
        public async Task<Response<List<EntTipoDeMantenimiento>>> DGetList()
        {
            var response = new Response<List<EntTipoDeMantenimiento>>();
            try
            {
                var items = await dbContext.TipoDeMantenimiento.AsNoTracking().Where(x => !x.bEliminado).OrderBy(x => x.Nombre).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntTipoDeMantenimiento>>(items));
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
        public async Task<Response<List<EntTipoDeMantenimiento>>> DGetListActive()
        {
            var response = new Response<List<EntTipoDeMantenimiento>>();
            try
            {
                var items = await dbContext.TipoDeMantenimiento.AsNoTracking().Where(x => !x.bEliminado && x.Estatus).OrderBy(x => x.Nombre).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntTipoDeMantenimiento>>(items));
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
