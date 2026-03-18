using AutoMapper;
using Mapeamentos_ConstructUsing.DTOs;
using Mapeamentos_ConstructUsing.Entities;

namespace Mapeamentos_ConstructUsing.Profiles;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<Funcionario, FuncionarioDTO>()
            .ConstructUsing(src => new FuncionarioDTO(src.Id, src.Nome, src.Salario, "Ativo"));
    }
}
