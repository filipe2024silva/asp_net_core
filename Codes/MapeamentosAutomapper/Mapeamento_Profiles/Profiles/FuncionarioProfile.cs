using AutoMapper;
using Mapeamento_Profiles.DTOs;
using Mapeamento_Profiles.Entities;

namespace Mapeamento_Profiles.Profiles;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<Funcionario, FuncionarioDTO>()
                .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.CargoAtual, opt => opt.MapFrom(src => src.Cargo))
                .ForMember(dest => dest.SalarioAnual, opt => opt.MapFrom(src => src.Salario * 12));
    }
}
