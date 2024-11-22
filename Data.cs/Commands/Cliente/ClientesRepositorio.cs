
using AutoMapper;
using Data.cs;
using Data.cs.Entities;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class ClientesRepositorio : IClientesRepositorio
{
    private readonly ApplicationDbContext dbContext;
    private readonly IHttpContextAccessor httpContext;
    private readonly IMapper _mapper;
    public ClientesRepositorio(ApplicationDbContext dbContext, IHttpContextAccessor httpContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.httpContext = httpContext;
        _mapper = mapper;
    }

    public async Task<Response<EntClientes>> DSave(EntClientes entity)
    {
        var response = new Response<EntClientes>();
        try
        {
            var newItem = _mapper.Map<Clientes>(entity);
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

    public async Task<Response<EntClientes>> DGetById(Guid iKey)
    {
        var response = new Response<EntClientes>();
        try
        {
            var entity = await dbContext.Clientes.AsNoTracking()
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

    public async Task<Response<List<EntClientes>>> DGetByName(string nombre)
    {
        var response = new Response<List<EntClientes>>();
        try
        {
            var items = await dbContext.Clientes.Where(c => c.sNombre.Contains(nombre)).ToListAsync();
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntClientes>>(items));
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

    public async Task<Response<List<EntClientes>>> DGetByEmail(string sEmail)
    {
        var response = new Response<List<EntClientes>>();
        try
        {
            var items = await dbContext.Clientes.Where(c => c.sEmail==sEmail).ToListAsync();
            if (items.Count > 0)
                response.SetSuccess(_mapper.Map<List<EntClientes>>(items));
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
            var items = await dbContext.Clientes.ToListAsync();
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
}