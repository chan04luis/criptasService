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
    public class ServiciosRepositorio:IServiciosRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiciosRepositorio> _logger;

        public ServiciosRepositorio(ApplicationDbContext dbContext, IMapper mapper, ILogger<ServiciosRepositorio> logger)
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
                var exitsKey = await dbContext.Servicios.AnyAsync(i => i.Id == pKey && !i.Eliminado);
                if (exitsKey)
                {
                    response.SetSuccess(exitsKey, "Servicio ya existente");
                }
                else
                {
                    response.SetError("No existe el Servicio");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExistKey));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> AnyExitNameAndKey(EntServicios pEntity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var existName = await dbContext.Servicios.AnyAsync(i => (i.Id != pEntity.Id) && (i.Nombre.Equals(pEntity.Nombre)));

                if (existName)
                {
                    response.SetSuccess(existName, "Servicio ya existente");
                }
                else
                {
                    response.SetError("No existe el servicio");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(AnyExitNameAndKey));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DAnyExistName(string nombre)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var existName = await dbContext.Servicios.AnyAsync(i => i.Nombre.Equals(nombre) && !i.Eliminado);

                if (existName)
                {
                    response.SetSuccess(existName, "Servicio ya existente");
                }
                else
                {
                    response.SetError("No existe el servicio");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DAnyExistName));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntServicios>> DSave(EntServicios entity)
        {
            var response = new Response<EntServicios>();
            try
            {
                var newItem = _mapper.Map<Servicios>(entity);
                newItem.Id = Guid.NewGuid();
                newItem.Nombre = entity.Nombre;
                newItem.Descripcion = entity.Descripcion;
                newItem.Estatus = true;
                newItem.Img = entity.Img;
                newItem.Eliminado = false;
                newItem.FechaRegistro = DateTime.Now.ToLocalTime();
                dbContext.Servicios.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntServicios>(newItem));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DSave));
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntServicios>> DUpdate(EntServicios entity)
        {
            Response<EntServicios> response = new Response<EntServicios>();

            try
            {
                var bEntity = await dbContext.Servicios.FindAsync(entity.Id);
                if (bEntity == null)
                {
                    response.SetError("El servicio no fue encontrado.");
                    return response;
                }
                bEntity.Nombre = entity.Nombre;
                bEntity.Descripcion = entity.Descripcion;
                bEntity.Estatus = entity.Estatus;
                if (entity.Img != null)
                    bEntity.Img = entity.Img;
                bEntity.FechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Update(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntServicios>(bEntity), "Actualizado correctamente");
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

        public async Task<Response<EntServicios>> DUpdateEstatus(EntServicios entity)
        {
            Response<EntServicios> response = new Response<EntServicios>();

            try
            {
                var bEntity = await dbContext.Servicios.FindAsync(entity.Id);
                bEntity.Estatus = entity.Estatus;
                bEntity.FechaActualizacion = DateTime.Now.ToLocalTime();
                dbContext.Attach(bEntity);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                    response.SetSuccess(_mapper.Map<EntServicios>(bEntity), "Actualizado correctamente");
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
                var bEntity = await dbContext.Servicios.FindAsync(uId);
                bEntity.Eliminado = true;
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

        public async Task<Response<EntServicios>> DGetById(Guid iKey)
        {
            var response = new Response<EntServicios>();
            try
            {
                var entity = await dbContext.Servicios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == iKey && !x.Eliminado);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntServicios>(entity));
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

        public async Task<Response<List<EntServicios>>> DGetList()
        {
            var response = new Response<List<EntServicios>>();
            try
            {
                var items = await dbContext.Servicios.AsNoTracking().Where(x => !x.Eliminado).OrderBy(x => x.Nombre).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntServicios>>(items));
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

        public async Task<Response<List<EntServicios>>> DGetListActive()
        {
            var response = new Response<List<EntServicios>>();
            try
            {
                var items = await dbContext.Servicios.AsNoTracking().Where(x => !x.Eliminado && x.Estatus).OrderBy(x => x.Nombre).ToListAsync();
                if (items.Count > 0)
                    response.SetSuccess(_mapper.Map<List<EntServicios>>(items));
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
