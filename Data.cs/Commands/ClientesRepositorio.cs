
using AutoMapper;
using Data.cs;
using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Request.Clientes;
using Models.Responses.Criptas;
using System.Drawing.Printing;
using Utils;

public class ClientesRepositorio : IClientesRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public ClientesRepositorio(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Response<EntClientes>> DSave(EntClientes entity)
    {
        var response = new Response<EntClientes>();
        try
        {
            var newItem = _mapper.Map<Clientes>(entity);
            newItem.bEliminado = false;
            dbContext.Clientes.Add(newItem);
            int i = await dbContext.SaveChangesAsync();
            if (i == 0)
            {
                response.HttpCode = System.Net.HttpStatusCode.BadRequest;
                response.SetError("Error al guardar el registro");
            }
            else
            {
                response.SetSuccess(_mapper.Map<EntClientes>(newItem));
            }

        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }

    public async Task<Response<EntClientes>> DUpdate(EntClientes entity)
    {
        Response<EntClientes> response = new Response<EntClientes>();

        try
        {
            var bEntity = dbContext.Clientes.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
            bEntity.sNombre=entity.sNombre;
            bEntity.sApellidos=entity.sApellidos;
            bEntity.sTelefono = entity.sTelefono;
            bEntity.sDireccion = entity.sDireccion;
            bEntity.sEmail = entity.sEmail;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(_mapper.Map<EntClientes>(bEntity), "Actualizado correctamente");
            else
            {
                response.SetError("Registro no actualizado");
                response.HttpCode = System.Net.HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }

    public async Task<Response<EntClientes>> DUpdateBoolean(EntClientes entity)
    {
        Response<EntClientes> response = new Response<EntClientes>();

        try
        {
            var bEntity = dbContext.Clientes.AsNoTracking().FirstOrDefault(x => x.uId == entity.uId);
            bEntity.bEstatus = entity.bEstatus;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(_mapper.Map<EntClientes>(bEntity), "Actualizado correctamente");
            else
            {
                response.SetError("Registro no actualizado");
                response.HttpCode = System.Net.HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }

    public async Task<Response<EntClientes>> DUpdateToken(Guid uId, String? sTokenFireBase)
    {
        Response<EntClientes> response = new Response<EntClientes>();

        try
        {
            var bEntity = dbContext.Clientes.AsNoTracking().FirstOrDefault(x => x.uId == uId);
            bEntity.sFcmToken = sTokenFireBase;
            bEntity.dtFechaActualizacion = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(_mapper.Map<EntClientes>(bEntity), "Actualizado correctamente");
            else
            {
                response.SetError("Registro no actualizado");
                response.HttpCode = System.Net.HttpStatusCode.BadRequest;
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
        Response<bool> response = new Response<bool>();

        try
        {
            var bEntity = dbContext.Clientes.AsNoTracking().FirstOrDefault(x => x.uId == uId);
            bEntity.bEliminado = true;
            bEntity.dtFechaEliminado = DateTime.Now.ToLocalTime();
            dbContext.Update(bEntity);
            var exec = await dbContext.SaveChangesAsync();

            if (exec > 0)
                response.SetSuccess(true, "bEliminado correctamente");
            else
            {
                response.SetError("Registro no eliminado");
                response.HttpCode = System.Net.HttpStatusCode.BadRequest;
            }
        }
        catch (Exception ex)
        {
            response.SetError(ex);
        }
        return response;
    }

    public async Task<Response<EntClientes>> DGetById(Guid iKey)
    {
        var response = new Response<EntClientes>();
        try
        {
            var entity = await dbContext.Clientes.AsNoTracking().Where(x => x.bEliminado == false).AsNoTracking()
            .SingleOrDefaultAsync(x => x.uId == iKey);
            if (entity != null)
                response.SetSuccess(_mapper.Map<EntClientes>(entity));
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

    public async Task<Response<PagedResult<EntClientes>>> DGetByFilters(EntClienteSearchRequest filtros)
    {
        var response = new Response<PagedResult<EntClientes>>();
        try
        {
            var items = dbContext.Clientes.AsNoTracking().Where(c => c.bEliminado == false);
            if (filtros.sNombre != null)
            {
                items = items.Where(x => x.sNombre.ToLower().Contains(filtros.sNombre.ToLower()));
            }
            if (filtros.sApellido != null)
            {
                items = items.Where(x => x.sApellidos.ToLower().Contains(filtros.sApellido.ToLower()));
            }
            if (filtros.sCorreo != null)
            {
                items = items.Where(x => x.sEmail.ToLower().Contains(filtros.sCorreo.ToLower()));
            }
            if (filtros.bEstatus != null)
            {
                items = items.Where(x => x.bEstatus == filtros.bEstatus);
            }
            int totalRecords = await items.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / filtros.iNumReg);
            if (totalRecords > 0)
            {
                var result = await items
                    .OrderBy(x => x.sNombre)
                    .ThenBy(x => x.sApellidos)
                    .ThenBy(x => x.uId)
                    .Skip((filtros.iNumPag - 1) * filtros.iNumReg)
                    .Take(filtros.iNumReg)
                    .ToListAsync();
                var list = _mapper.Map<List<EntClientes>>(result);
                var resultado = new PagedResult<EntClientes>(list, totalRecords, filtros.iNumPag, filtros.iNumReg);

                response.SetSuccess(resultado);
            }
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

    public async Task<Response<EntClientes>> DGetByEmail(string sEmail, string? Contra=null)
    {
        var response = new Response<EntClientes>();
        try
        {
            var items = await dbContext.Clientes.AsNoTracking().FirstAsync(c => c.sEmail==sEmail && c.bEliminado==false);
            if (items != null)
            {
                var email = _mapper.Map<EntClientes>(items);
                if (Contra != null)
                    email.sContra = items.sContra;
                response.SetSuccess(email);
            }
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

    public async Task<Response<List<EntClientes>>> DGetList()
    {
        var response = new Response<List<EntClientes>>();
        try
        {
            var items = await dbContext.Clientes.AsNoTracking().Where(x=>x.bEliminado==false).ToListAsync();
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntClientes>>(items));
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

    public async Task<Response<List<MisCriptas>>> DGetMisCriptas(Guid uIdCliente)
    {
        var response = new Response<List<MisCriptas>>();
        try
        {
            var items = await (
                from c in dbContext.Criptas
                join s in dbContext.Secciones on c.uIdSeccion equals s.uId
                join z in dbContext.Zonas on s.uIdZona equals z.uId
                join i in dbContext.Iglesias on z.uIdIglesia equals i.uId
                where c.uIdCliente == uIdCliente && !c.bEliminado && !s.bEliminado && !z.bEliminado && !i.bEliminado
                select new MisCriptas
                {
                    uId = c.uId.ToString(),
                    sNombre = c.sNumero,
                    sIglesia = i.sNombre,
                    sNombreSeccion = s.sNombre,
                    sNombreZona = z.sNombre,
                    sLatitud = i.sLatitud,
                    sLongitud = i.sLongitud,
                    bPagado = !c.bDisponible && !c.bEstatus,
                    dPrecio = c.dPrecio,
                    iFallecidos = dbContext.Fallecidos
                        .Count(f => f.uIdCripta == c.uId && !f.bEliminado), 
                    iBeneficiarios = dbContext.Beneficiarios
                        .Count(b => b.uIdCripta == c.uId && !b.bEliminado),
                    iVisitas = (from v in dbContext.Visitas.AsNoTracking()
                                join f in dbContext.Fallecidos.AsNoTracking()
                                    on v.uIdCriptas equals f.uId
                                where !v.bEliminado && !f.bEliminado
                                select new EntVisitas
                                {
                                    uId = v.uId,
                                    sNombreVisitante = v.sNombreVisitante,
                                    sMensaje = v.sMensaje,
                                    uIdCriptas = f.uIdCripta,
                                    dtFechaRegistro = v.dtFechaRegistro,
                                    dtFechaActualizacion = v.dtFechaActualizacion,
                                    bEstatus = v.bEstatus,
                                    sNombreFallecido = f.sNombre + " " + f.sApellidos,
                                }).Count(b => b.uIdCriptas == c.uId),
                    dtFechaCompra = dbContext.Pagos
                        .Where(p => p.uIdCripta == c.uId)
                        .OrderBy(p => p.dtFechaPago)
                        .Select(p => (p.bPagado ? p.dtFechaPago : p.dtFechaLimite.Date) ?? DateTime.MinValue)
                        .FirstOrDefault()
                }
            ).ToListAsync();

            if (items.Any())
                response.SetSuccess(items);
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
}