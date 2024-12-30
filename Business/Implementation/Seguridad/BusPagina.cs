using AutoMapper;
using Business.Interfaces.Seguridad;
using Data.cs.Entities.Seguridad;
using Data.cs.Interfaces.Seguridad;
using Modelos.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Business.Implementation.Seguridad
{
    public class BusPagina:IBusPagina
    {
        private readonly IPaginasRespositorio _datPagina;
        private readonly IMapper mapeador;

        public BusPagina(IMapper mapeador, IPaginasRespositorio _datPagina)
        {
            this.mapeador = mapeador;
            this._datPagina = _datPagina;
        }
        public async Task<Response<PaginaModelo>> BCreate(PaginaModelo createModel)
        {
            Response<PaginaModelo> response = new Response<PaginaModelo>();

            try
            {
                Response<List<Pagina>> obtenerPagina = await _datPagina.DGet();
                if (obtenerPagina.HasError)
                {
                    return response.GetResponse(obtenerPagina);
                }
                foreach (Pagina pagina in obtenerPagina.Result)
                {
                    if (pagina.sClavePagina.ToUpper() == createModel.sClavePagina.ToUpper())
                    {
                        return response.GetBadRequest("La clave de la página ya existe, intente con otra clave");
                    }
                }

                Pagina EntUsuario = mapeador.Map<Pagina>(createModel);

                var resp = await _datPagina.DSave(EntUsuario);

                if (resp.HasError)
                {
                    response.SetError("No se guardó la página");
                }
                else
                {
                    PaginaModelo EntUsuarioCreado = mapeador.Map<PaginaModelo>(resp.Result);
                    response.SetCreated(EntUsuarioCreado);
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
                var resData = await _datPagina.DDelete(iKey);
                response.SetSuccess(resData.Result);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;

        }
        public async Task<Response<PaginaModelo>> BGet(Guid iKey)
        {
            Response<PaginaModelo> response = new Response<PaginaModelo>();

            try
            {
                var resData = await _datPagina.DGet(iKey);
                PaginaModelo EntPagina = mapeador.Map<PaginaModelo>(resData.Result);
                response.SetSuccess(EntPagina);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<List<PaginaModelo>>> BGetAll()
        {
            Response<List<PaginaModelo>> response = new Response<List<PaginaModelo>>();
            try
            {
                var resData = await _datPagina.DGet();
                var listadoMapeador = mapeador.Map<List<PaginaModelo>>(resData.Result);
                response.SetSuccess(listadoMapeador);
            }
            catch (Exception ex)
            {
                response.SetError(ex);
            }
            return response;
        }
        public async Task<Response<PaginaModelo>> BUpdate(PaginaModelo updateModel)
        {
            Response<PaginaModelo> response = new Response<PaginaModelo>();
            try
            {
                Response<List<Pagina>> obtenerPagina = await _datPagina.DGet();
                if (obtenerPagina.HasError)
                {
                    return response.GetResponse(obtenerPagina);
                }
                foreach (Pagina pagina in obtenerPagina.Result)
                {
                    if (pagina.sClavePagina.ToUpper() == updateModel.sClavePagina.ToUpper() && pagina.uIdPagina != updateModel.uIdPagina)
                    {
                        return response.GetBadRequest("La clave de la página ya existe, intente con otra clave");
                    }
                }

                Pagina EntPagina = mapeador.Map<Pagina>(updateModel);

                var resp = await _datPagina.DUpdate(EntPagina);

                if (resp.HasError)
                {
                    response.SetError("No se modificó");
                }
                else
                {
                    PaginaModelo EntPaginaActualizado = mapeador.Map<PaginaModelo>(resp.Result);
                    response.SetSuccess(EntPaginaActualizado);
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
