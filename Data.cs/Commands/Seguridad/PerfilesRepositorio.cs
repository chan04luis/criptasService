using Data.cs.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Commands.Seguridad
{
    public class PerfilesRepositorio: IPerfilesRepositorio
    {
        private readonly ApplicationDbContext dbContext;
        public PerfilesRepositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Response<bool>> AnyExistKey(Guid pKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsKey = await dbContext.Perfiles.AnyAsync(i => i.id == pKey && i.Activo == true);

                response.SetSuccess(exitsKey, "Perfil ya existente");
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitNameAndKey(Perfil pEntity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsName = await dbContext.Perfiles.AnyAsync(i => (i.id != pEntity.id)
                                                                        && (i.NombrePerfil.Equals(pEntity.NombrePerfil)) && i.Activo == true);

                response.SetSuccess(exitsName, "Pefiles ya existente");
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> AnyExitName(string pName)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var exitsName = await dbContext.Perfiles.AnyAsync(i => i.NombrePerfil.Equals(pName) && i.Activo == true);

                response.SetSuccess(exitsName, "Perfil ya existente");
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> Delete(Guid iKey)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                var entity = await dbContext.Perfiles.FindAsync(iKey);

                entity.Activo = false;

                dbContext.Update(entity);

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(false, "Eliminado correctamente");
                }

                else
                {
                    response.SetError("Registro no eliminado");
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<List<Perfil>>> GetAll()
        {
            Response<List<Perfil>> response = new Response<List<Perfil>>();

            try
            {
                List<Perfil> result = await dbContext.Perfiles.AsNoTracking().Where(i => i.Activo == true).OrderBy(i => i.NombrePerfil).ToListAsync();

                if (result.Count <= 0)
                {
                    response.SetSuccess(new List<Perfil>(), "No se encontraron resultados");
                }
                else
                {
                    response.SetSuccess(result, "Consultado correctamente");
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<Perfil>> Get(Guid iKey)
        {
            Response<Perfil> response = new Response<Perfil>();

            try
            {
                var result = await dbContext.Perfiles.AsNoTracking().FirstOrDefaultAsync(i => i.id == iKey && i.Activo == true);

                if (result == null)
                {
                    response.SetSuccess(new Perfil(), "No se encontraron resultados");
                }
                else
                {
                    response.SetSuccess(result, "Consultado Correctamente");
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<Perfil>> Save(Perfil newItem)
        {
            Response<Perfil> response = new Response<Perfil>();

            try
            {
                newItem.id = Guid.NewGuid();
                newItem.Activo = true;

                dbContext.Perfiles.Add(newItem);
                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(newItem, "Agregado correctamente");
                }
                else
                {
                    response.SetError("Registro no agregado");
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> Update(Perfil entity)
        {
            Response<bool> response = new Response<bool>();

            try
            {
                dbContext.Attach(entity);
                dbContext.Entry(entity).Property(x => x.ClavePerfil).IsModified = true;
                dbContext.Entry(entity).Property(x => x.NombrePerfil).IsModified = true;

                var exec = await dbContext.SaveChangesAsync();

                if (exec > 0)
                {
                    response.SetSuccess(true, "Actualizado correctamente");
                }

                else
                {
                    response.SetError("Registro no actualizado");
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
    }
}
