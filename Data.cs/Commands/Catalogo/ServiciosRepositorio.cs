using AutoMapper;
using Data.cs.Entities.Catalogos;
using Data.cs.Interfaces.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models;
using Models.Responses.Servicio;
using Utils;

namespace Data.cs.Commands.Catalogo
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
                newItem.ImgPreview = entity.ImgPreview;
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
                bEntity.ImgPreview = entity.ImgPreview;
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
                bEntity.Estatus = entity.Estatus.Value;
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

        public async Task<Response<List<EntServiceItem>>> DGetListActive(Guid uIdSucursal)
        {
            var response = new Response<List<EntServiceItem>>();

            try
            {
                var itemsQuery = from s in dbContext.Servicios.AsNoTracking()
                                 join ss in dbContext.ServiciosSucursales.AsNoTracking() on s.Id equals ss.IdServicio
                                      where !s.Eliminado && s.Estatus && ss.IdSucursal == uIdSucursal
                                 orderby s.Nombre
                                 select new EntServiceItem
                                 {
                                     Id = s.Id,
                                     Nombre = s.Nombre,
                                     ImgPreview = s.ImgPreview,
                                     Asignado=true
                                 };

                var items = await itemsQuery.ToListAsync();

                if (items.Any())
                {
                    response.SetSuccess(items);
                    response.HttpCode = System.Net.HttpStatusCode.OK;
                }
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
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<Response<List<EntServiceItem>>> DGetListPreAssigment(Guid uIdSucursal)
        {
            var response = new Response<List<EntServiceItem>>();

            try
            {
                var itemsQuery = from s in dbContext.Servicios.AsNoTracking()
                                 join ss in dbContext.ServiciosSucursales.AsNoTracking().Where(x => x.IdSucursal==uIdSucursal)
                                 on s.Id equals ss.IdServicio into servicioSucursalGroup
                                 from ss in servicioSucursalGroup.DefaultIfEmpty()
                                 where !s.Eliminado && s.Estatus
                                 orderby s.Nombre
                                 select new EntServiceItem
                                 {
                                     Id = s.Id,
                                     Nombre = s.Nombre,
                                     ImgPreview = s.ImgPreview,
                                     IdAsignado = ss == null ? null : ss.Id,
                                     Asignado = ss == null ? false : ss.Asignado
                                 };

                var items = await itemsQuery.ToListAsync();

                if (items.Any())
                {
                    response.SetSuccess(items);
                    response.HttpCode = System.Net.HttpStatusCode.OK;
                }
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
                response.HttpCode = System.Net.HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public async Task<Response<bool>> DSaveToSucursal(List<EntServiceItem> entities, Guid uIdSucursal)
        {
            var response = new Response<bool>();
            try
            {
                var itemsUpdate = entities.Where(x => x.IdAsignado != null);
                foreach(var item in itemsUpdate)
                {
                    var dbItem = await dbContext.ServiciosSucursales.FindAsync(item.IdAsignado);
                    if(dbItem != null)
                    {
                        dbItem.Asignado = item.Asignado;
                        dbItem.FechaActualizacion = DateTime.Now.ToLocalTime();
                        _logger.LogWarning("{dbItem}", dbItem);
                        dbContext.ServiciosSucursales.Update(dbItem);
                    }
                }

                var itemsAdd = entities.Where(x => x.IdAsignado == null).Select(x => new ServiciosSucursales()
                {
                    Id = Guid.NewGuid(),
                    IdServicio = x.Id.Value,
                    IdSucursal = uIdSucursal,
                    FechaRegistro = DateTime.Now.ToLocalTime(),
                    FechaActualizacion = DateTime.Now.ToLocalTime(),
                    Asignado = x.Asignado
                }).ToList();
                _logger.LogWarning("{itemsAdd}", itemsAdd);
                if (itemsAdd.Count > 0)
                    dbContext.ServiciosSucursales.AddRange(itemsAdd);

                int i = dbContext.SaveChanges();
                if (i == 0)
                {
                    response.Result = false;
                    response.Message = "Error al guardar el registro";
                    response.HasError = false;
                    response.HttpCode = System.Net.HttpStatusCode.NotModified;
                }
                else
                {
                    response.SetSuccess(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al ejecutar el método {MethodName}", nameof(DSave));
                response.SetError(ex);
            }
            return response;
        }

    }
}
