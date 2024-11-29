using AutoMapper;
using Data.cs.Entities;
using Entities.JsonRequest.Clientes;
using Entities.JsonRequest.Iglesias;
using Entities.JsonRequest.Usuarios;
using Entities.JsonRequest.Zonas;
using Entities.Models;
using Entities.Request.Criptas;
using Entities.Request.Fallecidos;
using Entities.Request.Pagos;
using Entities.Request.Secciones;
using Entities.Request.TipoPagos;
using Entities.Request.Usuarios;
using Entities.Responses.Iglesia;
using Entities.Responses.Usuarios;
using Entities.Responses.Zonas;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Clientes
        CreateMap<EntClientes, Clientes>();
        CreateMap<Clientes, EntClientes>()
            .ForMember(dest => dest.sFechaNacimiento, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.sFechaNacimiento) ? "1900-01-01" : src.sFechaNacimiento))
            .ForMember(dest => dest.iEdad, opt => opt.MapFrom(src => CalcularEdad(src.sFechaNacimiento)));
        CreateMap<EntClienteUpdateRequest, EntClientes>();
        CreateMap<EntClienteUpdateEstatusRequest, EntClientes>();
        #endregion

        #region Iglesias
        CreateMap<EntIglesias, Iglesias>();
        CreateMap<Iglesias, EntIglesias>();
        CreateMap<Iglesias, EntIglesiaResponse>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.listZonas));
        CreateMap<EntIglesiaUpdateRequest, EntIglesias>();
        CreateMap<EntIglesiaUpdateEstatusRequest, EntIglesias>();
        #endregion

        #region Zonas
        CreateMap<EntZonas, Zonas>();
        CreateMap<Zonas, EntZonas>();
        CreateMap<Zonas, EntZonasResponse>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.listSecciones));
        CreateMap<EntZonaUpdateRequest, EntZonas>();
        CreateMap<EntZonaUpdateEstatusRequest, EntZonas>();
        #endregion

        #region Secciones
        CreateMap<EntSecciones, Secciones>();
        CreateMap<Secciones, EntSecciones>();
        CreateMap<EntSeccionesUpdateRequest, EntSecciones>();
        CreateMap<EntSeccionesUpdateEstatusRequest, EntSecciones>();
        #endregion

        #region Criptas
        CreateMap<EntCriptas, Criptas>();
        CreateMap<Criptas, EntCriptas>();
        CreateMap<EntCriptaUpdateRequest, EntCriptas>();
        CreateMap<EntCriptaUpdateEstatusRequest, EntCriptas>();
        #endregion

        #region Tipos de pagos
        CreateMap<EntTiposPago, TiposDePago>();
        CreateMap<TiposDePago, EntTiposPago>();
        CreateMap<EntTiposPagoRequest, EntTiposPago>();
        CreateMap<EntTiposPagoSearchRequest, EntTiposPago>();
        CreateMap<EntTiposPago, EntTiposPagoRequest>();
        #endregion

        #region Pagos
        CreateMap<EntPagos, Pagos>();
        CreateMap<Pagos, EntPagos>();
        CreateMap<EntPagosRequest, EntPagos>();
        CreateMap<EntPagosSearchRequest, EntPagos>();
        CreateMap<EntPagosUpdateEstatusRequest, EntPagos>();
        #endregion

        #region PagosParciales
        CreateMap<EntPagosParciales, PagosParciales>().ReverseMap();
        #endregion

        #region Criptas
        CreateMap<EntCriptas, Criptas>();
        CreateMap<Criptas, EntCriptas>();
        CreateMap<EntCriptaUpdateRequest, EntCriptas>();
        CreateMap<EntCriptaUpdateEstatusRequest, EntCriptas>();
        #endregion

        #region Visitas
        CreateMap<EntVisitas, Visitas>();
        CreateMap<Visitas, EntVisitas>();
        #endregion

        #region Fallecidos
        CreateMap<EntFallecidos, Fallecidos>().ReverseMap();
        CreateMap<EntFallecidos, EntFallecidosRequest>().ReverseMap();
        CreateMap<EntFallecidosUpdateRequest, EntFallecidos>().ReverseMap();
        CreateMap<EntFallecidosUpdateEstatusRequest, EntFallecidos>().ReverseMap();
        #endregion

        #region Beneficiarios
        CreateMap<EntBeneficiarios, Beneficiarios>().ReverseMap();
        CreateMap<EntBeneficiarios, EntBeneficiariosRequest>().ReverseMap();
        CreateMap<EntBeneficiariosUpdateRequest, EntBeneficiarios>().ReverseMap();
        CreateMap<EntBeneficiariosUpdateEstatusRequest, EntBeneficiarios>().ReverseMap();
        #endregion

        #region Usuarios

        CreateMap<EntUsuarios, Usuarios>().ReverseMap();
        CreateMap<EntUsuarios, AuthLogin>().ReverseMap();
        CreateMap<EntUsuarios, EntUsuarioRequest>().ReverseMap();
        CreateMap<EntUsuarioUpdateRequest, EntUsuarios>().ReverseMap();
        CreateMap<EntUsuarioUpdateEstatusRequest, EntUsuarios>().ReverseMap();

        #endregion


    }

    private int? CalcularEdad(string fechaNacimiento)
    {
        if (DateTime.TryParse(fechaNacimiento, out DateTime fechaNac))
        {
            var edad = DateTime.Today.Year - fechaNac.Year;
            if (fechaNac.Date > DateTime.Today.AddYears(-edad)) edad--;
            return edad;
        }
        return null;
    }
}