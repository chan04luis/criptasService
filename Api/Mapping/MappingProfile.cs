using AutoMapper;
using Data.cs.Entities;
using Entities.JsonRequest.Clientes;
using Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EntClientes, Clientes>();
        CreateMap<Clientes, EntClientes>();


        CreateMap<EntClienteUpdateRequest, EntClientes>();
        CreateMap<EntClientes, EntClienteUpdateRequest> ();
    }
}