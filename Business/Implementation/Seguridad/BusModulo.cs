using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Commands.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Entities;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation.Seguridad
{
    public class BusModulo:IBusModulo
    {
        private readonly IModulosRepositorio _datModulo;
        private readonly IMapper mapeador;
        public BusModulo(IMapper mapeador, IModulosRepositorio _datModulo)
        {
            this.mapeador = mapeador;
            this._datModulo = _datModulo;
        }
        public async Task<Response<ModuloModelo>> BCreate(ModuloModelo createModel)
        {
            Response<ModuloModelo> response = new Response<ModuloModelo>();
            try
            {
                Response<List<Modulo>> obtenerModulo = await _datModulo.DGet();
                if (obtenerModulo.HasError)
                {
                    return response.GetResponse(obtenerModulo);
                }
                foreach (Modulo modulo in obtenerModulo.Result)
                {
                    if (modulo.sClaveModulo.ToUpper() == createModel.sClaveModulo.ToUpper())
                    {
                        return response.GetBadRequest("La clave del módulo ya existe, intente con otra clave");
                    }
                }

                Modulo entModulo = mapeador.Map<Modulo>(createModel);

                var resp = await _datModulo.DSave(entModulo);

                if (resp.HasError)
                {
                    response.SetError("Not Created");
                }
                else
                {
                    ModuloModelo EntModuloCreado = mapeador.Map<ModuloModelo>(resp.Result);
                    response.SetCreated(EntModuloCreado);
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
                var resData = await _datModulo.DDelete(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<ModuloModelo>> BGet(Guid iKey)
        {
            Response<ModuloModelo> response = new Response<ModuloModelo>();
            try
            {
                var resData = await _datModulo.DGet(iKey);
                ModuloModelo EntModulo = mapeador.Map<ModuloModelo>(resData.Result);
                response.SetSuccess(EntModulo);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<List<ModuloModelo>>> BGetAll()
        {
            Response<List<ModuloModelo>> response = new Response<List<ModuloModelo>>();
            try
            {
                var resData = await _datModulo.DGet();
                var listadoMapeador = mapeador.Map<List<ModuloModelo>>(resData.Result);
                response.SetSuccess(listadoMapeador);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<ModuloModelo>> BUpdate(ModuloModelo updateModel)
        {
            Response<ModuloModelo> response = new Response<ModuloModelo>();
            try
            {
                Response<List<Modulo>> obtenerModulo = await _datModulo.DGet();
                if (obtenerModulo.HasError)
                {
                    return response.GetResponse(obtenerModulo);
                }
                foreach (Modulo modulo in obtenerModulo.Result)
                {
                    if (modulo.sClaveModulo.ToUpper() == updateModel.sClaveModulo.ToUpper() && modulo.uIdModulo != updateModel.uIdModulo)
                    {
                        return response.GetBadRequest("La clave del módulo ya existe, intente con otra clave");
                    }
                }

                Modulo entModulo = mapeador.Map<Modulo>(updateModel);

                var resp = await _datModulo.DUpdate(entModulo);
                if (resp.HasError)
                {
                    response.SetError("No se modificó");
                }
                else
                {
                    ModuloModelo EntModuloActualizado = mapeador.Map<ModuloModelo>(resp.Result);
                    response.SetSuccess(EntModuloActualizado);
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
