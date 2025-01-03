using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Models.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Seguridad
{
    public class BusBoton:IBusBoton
    {
        private readonly IBotonesRepositorio botonesRepositorio;
        private readonly IMapper mapeador;

        public BusBoton(IMapper mapeador, IBotonesRepositorio botonesRepositorio)
        {
            this.mapeador = mapeador;
            this.botonesRepositorio = botonesRepositorio;
        }
        public async Task<Response<BotonModelo>> BCreate(BotonModelo createModel)
        {
            Response<BotonModelo> response = new Response<BotonModelo>();

            try
            {
                Response<List<Boton>> obtenerBoton = await botonesRepositorio.DGet();
                if (obtenerBoton.HasError)
                {
                    return response.GetResponse(obtenerBoton);
                }
                foreach (Boton boton in obtenerBoton.Result)
                {
                    if (boton.sClaveBoton.ToUpper() == createModel.sClaveBoton.ToUpper())
                    {
                        return response.GetBadRequest("La clave del botón ya existe, intente con otra clave");
                    }
                }

                Boton entBoton = mapeador.Map<Boton>(createModel);

                var resp = await botonesRepositorio.DSave(entBoton);

                if (resp.HasError)
                {
                    response.SetError("No se guardó el botón");
                }
                else
                {
                    BotonModelo entBotonCreado = mapeador.Map<BotonModelo>(resp.Result);
                    response.SetCreated(entBotonCreado);
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
                var resData = await botonesRepositorio.DDelete(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<BotonModelo>> BGet(Guid iKey)
        {
            Response<BotonModelo> response = new Response<BotonModelo>();

            try
            {
                var resData = await botonesRepositorio.DGet(iKey);
                BotonModelo entBoton = mapeador.Map<BotonModelo>(resData.Result);
                response.SetSuccess(entBoton);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }

            return response;
        }
        public async Task<Response<List<BotonModelo>>> BGetAll()
        {
            Response<List<BotonModelo>> response = new Response<List<BotonModelo>>();
            try
            {
                var resData = await botonesRepositorio.DGet();
                var listadoMapeador = mapeador.Map<List<BotonModelo>>(resData.Result);
                response.SetSuccess(listadoMapeador);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<bool>> BUpdate(BotonModelo updateModel)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                Response<List<Boton>> obtenerBoton = await botonesRepositorio.DGet();
                if (obtenerBoton.HasError)
                {
                    return response.GetResponse(obtenerBoton);
                }
                foreach (Boton boton in obtenerBoton.Result)
                {
                    if (boton.sClaveBoton.ToUpper() == updateModel.sClaveBoton.ToUpper() && boton.uIdBoton != updateModel.uIdBoton)
                    {
                        return response.GetBadRequest("La clave del botón ya existe, intente con otra clave");
                    }
                }

                Boton entBoton = mapeador.Map<Boton>(updateModel);

                var resp = await botonesRepositorio.DUpdate(entBoton);

                if (resp.HasError)
                {
                    response.SetError("No se modificó");
                }
                else
                {
                    
                    response.SetSuccess(resp.Result);
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
