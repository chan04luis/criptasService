using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Microsoft.Extensions.Logging;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Seguridad
{
    public class BusPerfiles:IBusPerfiles
    {
        private readonly IPerfilesRepositorio perfilesRepositorio;
        private readonly IMapper mapeador;

        public BusPerfiles(IMapper mapeador, IPerfilesRepositorio perfilesRepositorio)
        {
            this.mapeador = mapeador;
            this.perfilesRepositorio = perfilesRepositorio;
        }

        public async Task<Response<PerfilModelo>> BCreate(PerfilModelo createModel)
        {
            Response<PerfilModelo> response = new Response<PerfilModelo>();

            try
            {
                Response<bool> existName = await perfilesRepositorio.AnyExitName(createModel.NombrePerfil);
             
                if (existName.Result)
                {
                    response.SetError(existName.Message);
                    return response;
                }


                Perfil perfilEntidad = mapeador.Map<Perfil>(createModel);

                var perfilCreado = await perfilesRepositorio.Save(perfilEntidad);

                if (perfilCreado.HasError)
                {
                    response.SetError(perfilCreado.Message);
                }
                else
                {
                    PerfilModelo entInvitadoCreado = mapeador.Map<PerfilModelo>(perfilCreado.Result);
                    response.SetCreated(entInvitadoCreado);
                }
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> BDelete(Guid iKey)
        {
            Response<bool> response = new();
            try
            {
                var resData = await perfilesRepositorio.Delete(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<PerfilModelo>> BGet(Guid iKey)
        {
            Response<PerfilModelo> response = new Response<PerfilModelo>();
            try
            {
                var resData = await perfilesRepositorio.Get(iKey);

                PerfilModelo perfilModelo = mapeador.Map<PerfilModelo>(resData.Result);

                response.SetSuccess(perfilModelo);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<List<PerfilModelo>>> BGetAll()
        {
            Response<List<PerfilModelo>> response = new Response<List<PerfilModelo>>();
            try
            {
                var resData = await perfilesRepositorio.GetAll();
                var listadoMapeador = mapeador.Map<List<PerfilModelo>>(resData.Result);
                response.SetSuccess(listadoMapeador);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }

        public async Task<Response<PerfilModelo>> BUpdate(PerfilModelo updateModel)
        {
            Response<PerfilModelo> response = new Response<PerfilModelo>();
            try
            {
                Perfil entPerfil = mapeador.Map<Perfil>(updateModel);
                var perfilesExistentes = await perfilesRepositorio.AnyExitNameAndKey(entPerfil);
                if (perfilesExistentes.Result)
                {
                    response.SetError("Perfil ya existente.");
                    return response;
                }

                var result = await perfilesRepositorio.Update(entPerfil);

                if (result.Result)
                {
                    response.SetSuccess(updateModel, result.Message);
                }
                else
                {
                    response.SetError(result.Message);
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
