
using AutoMapper;
using Business.Data;
using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Models.Enums;
using Models.Models;
using Models.Request.Criptas;
using Models.Responses.Criptas;
using Models.Responses.Pagos;
using System.Linq;
using System.Net;
using Utils;

namespace Data.cs.Commands
{
    public class CriptasRepositorio : ICriptasRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper _mapper;

        public CriptasRepositorio(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Response<List<EntCriptas>>> DGetByName(string nombre, Guid uIdSeccion)
        {
            var response = new Response<List<EntCriptas>>();

            try
            {
                var criptas = await dbContext.Criptas.AsNoTracking()
                    .Where(c => c.sNumero == nombre && c.uIdSeccion == uIdSeccion && !c.bEliminado)
                    .ToListAsync();

                response.Result = _mapper.Map<List<EntCriptas>>(criptas);
                return response;
            }
            catch (Exception ex)
            {
                response.SetError($"Error al consultar criptas por nombre: {ex.Message}");
                response.HttpCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
        public async Task<Response<EntCriptas>> DSave(EntCriptas entity)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var newItem = _mapper.Map<Criptas>(entity);
                newItem.bEliminado = false;
                newItem.bDisponible = true;
                dbContext.Criptas.Add(newItem);
                int i = await dbContext.SaveChangesAsync();
                if (i == 0)
                {
                    response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    response.SetError("Error al guardar el registro");
                }
                else
                {
                    response.SetSuccess(_mapper.Map<EntCriptas>(newItem));
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntCriptas>> DUpdate(EntCriptas entity)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var bEntity = dbContext.Criptas.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.uIdSeccion = entity.uIdSeccion;
                    bEntity.uIdCliente = entity.uIdCliente;
                    bEntity.sNumero = entity.sNumero;
                    bEntity.dPrecio = entity.dPrecio;
                    bEntity.sUbicacionEspecifica = entity.sUbicacionEspecifica;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntCriptas>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Cripta no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntCriptas>> DUpdateBoolean(EntCriptas entity)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var bEntity = dbContext.Criptas.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.bEstatus = entity.bEstatus;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntCriptas>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Cripta no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<EntCriptas>> DUpdateDisponible(EntCriptas entity)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var bEntity = dbContext.Criptas.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
                if (bEntity != null)
                {
                    bEntity.bDisponible = entity.bDisponible;
                    bEntity.uIdCliente = entity.uIdCliente;
                    bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(_mapper.Map<EntCriptas>(bEntity), "Actualizado correctamente");
                    else
                    {
                        response.SetError("Registro no actualizado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Cripta no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<bool>> DUpdateEliminado(Guid uId)
        {
            var response = new Response<bool>();
            try
            {
                var bEntity = dbContext.Criptas.AsNoTracking().FirstOrDefault(x => x.uId == uId);
                if (bEntity != null)
                {
                    bEntity.bEliminado = true;
                    bEntity.dtFechaEliminado = DateTime.Now.ToLocalTime();
                    dbContext.Update(bEntity);
                    var exec = await dbContext.SaveChangesAsync();

                    if (exec > 0)
                        response.SetSuccess(true, "Eliminado correctamente");
                    else
                    {
                        response.SetError("Registro no eliminado");
                        response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.SetError("Cripta no encontrada");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<EntCriptas>> DGetById(Guid iKey)
        {
            var response = new Response<EntCriptas>();
            try
            {
                var entity = await dbContext.Criptas.AsNoTracking().SingleOrDefaultAsync(x => x.uId == iKey && !x.bEliminado);
                if (entity != null)
                    response.SetSuccess(_mapper.Map<EntCriptas>(entity));
                else
                {
                    response.SetError("Registro no encontrado");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<PagedResult<EntCriptasLista>>> DGetByFilters(EntCriptaSearchRequest filtros)
        {
            var response = new Response<PagedResult<EntCriptasLista>>();
            try
            {
                var query = (from c in dbContext.Criptas
                             join s in dbContext.Secciones on c.uIdSeccion equals s.uId
                             join z in dbContext.Zonas on s.uIdZona equals z.uId
                             join i in dbContext.Iglesias on z.uIdIglesia equals i.uId
                             join cl in dbContext.Clientes on c.uIdCliente equals cl.uId into ClienteJoin
                             from cl in ClienteJoin.DefaultIfEmpty() // LEFT JOIN con Clientes
                             where !c.bEliminado
                             select new EntCriptasLista
                             {
                                 uId = c.uId,
                                 uIdSeccion = s.uId,
                                 sNombreSeccion = s.sNombre,
                                 uIdZona = z.uId,
                                 sNombreZona = z.sNombre,
                                 uIdIglesia = i.uId,
                                 sNombreIglesia = i.sNombre,
                                 uIdCliente = c.uIdCliente,
                                 sNombreCliente = cl != null ? cl.sNombre : "Sin Cliente",
                                 sApellidosCliente = cl != null ? cl.sApellidos ?? "" : "",
                                 sNumero = c.sNumero,
                                 dPrecio = c.dPrecio,
                                 sUbicacionEspecifica = c.sUbicacionEspecifica,
                                 dtFechaRegistro = c.dtFechaRegistro,
                                 dtFechaActualizacion = c.dtFechaActualizacion,
                                 bEstatus = c.bEstatus,
                                 bDisponible = c.bDisponible
                             });

                // Aplicar filtros dinámicos
                if (filtros.uIdCliente.HasValue)
                    query = query.Where(c => c.uIdCliente == filtros.uIdCliente);

                if (filtros.uIdSeccion.HasValue)
                    query = query.Where(c => c.uIdSeccion == filtros.uIdSeccion);

                if (filtros.uIdZona.HasValue)
                    query = query.Where(c => c.uIdZona == filtros.uIdZona);

                if (filtros.uIdIglesia.HasValue)
                    query = query.Where(c => c.uIdIglesia == filtros.uIdIglesia);

                if (!string.IsNullOrWhiteSpace(filtros.sNumero))
                    query = query.Where(c => c.sNumero.Contains(filtros.sNumero));

                if (!string.IsNullOrWhiteSpace(filtros.sUbicacionEspecifica))
                    query = query.Where(c => c.sUbicacionEspecifica.Contains(filtros.sUbicacionEspecifica));

                if (filtros.bEstatus.HasValue)
                    query = query.Where(c => c.bEstatus == filtros.bEstatus);

                // Paginación
                int totalRecords = await query.CountAsync();
                var resultList = await query
                    .OrderBy(c => c.sNombreIglesia)
                    .ThenBy(c => c.sNombreZona)
                    .ThenBy(c => c.sNombreSeccion)
                    .ThenBy(c => c.sNumero)
                    .Skip((filtros.iNumPag - 1) * filtros.iNumReg)
                    .Take(filtros.iNumReg)
                    .ToListAsync();

                // Verificar si hay registros
                if (resultList.Any())
                {
                    var resultado = new PagedResult<EntCriptasLista>(resultList, totalRecords, filtros.iNumPag, filtros.iNumReg);
                    response.SetSuccess(resultado);
                }
                else
                {
                    response.SetError("No se encontraron registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }

        public async Task<Response<List<EntCriptas>>> DGetList(Guid uIdSeccion)
        {
            var response = new Response<List<EntCriptas>>();
            try
            {
                var items = await dbContext.Criptas.AsNoTracking().Where(x => !x.bEliminado && x.uIdSeccion == uIdSeccion).OrderBy(x=>x.sNumero).ToListAsync();
                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntCriptas>>(items));
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<List<EntCriptas>>> DGetListDisponible(Guid uIdSeccion)
        {
            var response = new Response<List<EntCriptas>>();
            try
            {
                var items = await dbContext.Criptas.AsNoTracking().Where(x => !x.bEliminado && x.uIdSeccion == uIdSeccion && x.bDisponible && x.bEstatus).OrderBy(x => x.sNumero).ToListAsync();
                if (items.Any())
                    response.SetSuccess(_mapper.Map<List<EntCriptas>>(items));
                else
                {
                    response.SetError("Sin registros");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex.Message);
            }
            return response;
        }
        public async Task<Response<List<CriptasDisponibles>>> DGetListDisponibleByIglesia(Guid uId)
        {
            var response = new Response<List<CriptasDisponibles>>();

            try
            {
                var items = await (
                    from c in dbContext.Criptas
                    join s in dbContext.Secciones on c.uIdSeccion equals s.uId
                    join z in dbContext.Zonas on s.uIdZona equals z.uId
                    join i in dbContext.Iglesias on z.uIdIglesia equals i.uId
                    where i.uId == uId &&
                          c.bDisponible == true && !c.bEliminado &&
                          s.bEstatus == true && !s.bEliminado &&
                          z.bEstatus == true && !z.bEliminado
                    select new CriptasDisponibles
                    {
                        uId = c.uId,
                        sNombre = c.sNumero,
                        sIglesia = i.sNombre,
                        sNombreSeccion = s.sNombre,
                        sNombreZona = z.sNombre,
                        sLatitud = i.sLatitud ?? "0",
                        sLongitud = i.sLongitud ?? "0",
                        bEstatus = c.bEstatus,
                        bDisponible = c.bDisponible,
                        dPrecio = c.dPrecio,
                    }
                )
                .OrderBy(x => x.sNombreZona)
                .ThenBy(x => x.sNombreSeccion)
                .ThenBy(x => x.sNombre)
                .ToListAsync();

                if (items.Any())
                    response.SetSuccess(items);
                else
                {
                    response.SetError("No hay criptas disponibles para esta iglesia.");
                    response.HttpCode = System.Net.HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                response.SetError($"Error al obtener criptas disponibles: {ex.Message}");
            }

            return response;
        }
        public async Task<Response<CriptasResumen>> DGetResumenCriptas()
        {
            var response = new Response<CriptasResumen>();

            try
            {
                DateTime inicioMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);

                var total = await dbContext.Criptas
                    .CountAsync(c => !c.bEliminado);

                var disponibles = await dbContext.Criptas
                    .CountAsync(c => !c.bEliminado && c.bDisponible);

                var ocupadas = await dbContext.Criptas
                    .CountAsync(c => !c.bEliminado && !c.bDisponible);

                var clientes = await dbContext.Clientes
                    .CountAsync(c => !c.bEliminado && c.uId != Guid.Parse(IdPermanentes.clienteGeneral.GetDescription()));

                var ventas = await dbContext.Pagos
                    .CountAsync(c => !c.bEliminado && c.iTipoPago==1 && c.bPagado==true && c.dtFechaPago >= inicioMes && c.dtFechaPago <= finMes);

                var ingresos = await dbContext.Pagos
                    .Where(c => !c.bEliminado && c.bPagado == true && c.dtFechaPago >= inicioMes && c.dtFechaPago <= finMes)
                    .SumAsync(x=>x.dMontoPagado ?? 0);

                var ingresosParcial = await (from pago in dbContext.Pagos
                                             join parcial in dbContext.PagosParciales
                                             on pago.uId equals parcial.uIdPago
                                             where !pago.bEliminado
                                                   && pago.bPagado == false
                                                   && !parcial.bEliminado
                                                   && parcial.dtFechaPago >= inicioMes
                                                   && parcial.dtFechaPago <= finMes
                                             select parcial.dMonto)
                               .SumAsync();

                var resumen = new CriptasResumen
                {
                    Total = total,
                    Disponibles = disponibles,
                    Ocupadas = ocupadas,
                    Clientes = clientes,
                    Ingresos = $"{(ingresos + ingresosParcial):C}",
                    Ventas = ventas
                };

                response.SetSuccess(resumen);
            }
            catch (Exception ex)
            {
                response.SetError($"Error al obtener el resumen de criptas: {ex.Message}");
                response.HttpCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }

}
