using AutoMapper;
using Mapeamento_Resolvers.DTOs;
using Mapeamento_Resolvers.Entities;
using Mapeamento_Resolvers.Resolvers;

namespace Mapeamento_Resolvers.Profiles;


public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<Funcionario, FuncionarioDTO>()
          .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Nome))
          .ForMember(dest => dest.SalarioAnual, opt => opt.MapFrom<SalarioAnualResolver>()) // Resolver p/salário
          .ForMember(dest => dest.Idade, opt => opt.MapFrom<IdadeResolver>()); // Resolver para idade
    }
}
