using AutoMapper;
using Mapeamento_Profiles.DTOs;
using Mapeamento_Profiles.Entities;

namespace Mapeamento_Profiles.Profiles;

public class DepartamentoProfile : Profile
{
    public DepartamentoProfile()
    {
        CreateMap<Departamento, DepartamentoDTO>();
    }
}