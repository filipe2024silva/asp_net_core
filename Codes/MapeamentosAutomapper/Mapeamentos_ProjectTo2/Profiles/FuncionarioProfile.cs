using AutoMapper;
using Mapeamentos_ProjectTo2.DTOs;
using Mapeamentos_ProjectTo2.Entities;

namespace Mapeamentos_ProjectTo2.Profiles;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<Funcionario, FuncionarioDTO>();
    }
}
