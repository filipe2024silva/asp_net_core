using AutoMapper;
using Mapeamento_Resolvers.DTOs;
using Mapeamento_Resolvers.Entities;

namespace Mapeamento_Resolvers.Resolvers;

public class SalarioAnualResolver : IValueResolver<Funcionario, FuncionarioDTO, decimal>
{
    public decimal Resolve(Funcionario source, 
                           FuncionarioDTO destination, 
                           decimal destMember, 
                           ResolutionContext context)
    {
        return source.Salario * 12;
    }
}
