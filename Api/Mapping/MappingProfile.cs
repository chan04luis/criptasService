using AutoMapper;
using Data.cs.Entities;
using Entities.JsonRequest.Clientes;
using Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EntClientes, Clientes>();
        CreateMap<Clientes, EntClientes>()
            .ForMember(dest => dest.sFechaNacimiento, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.sFechaNacimiento) ? "1900-01-01" : src.sFechaNacimiento))
            .ForMember(dest => dest.iEdad, opt => opt.MapFrom(src => CalcularEdad(src.sFechaNacimiento)));


        CreateMap<EntClienteUpdateRequest, EntClientes>();
        CreateMap<EntClienteUpdateEstatusRequest, EntClientes>();


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