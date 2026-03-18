using AutoMapper;
using Mapeamento_Resolvers.DTOs;
using Mapeamento_Resolvers.Entities;

namespace Mapeamento_Resolvers.Resolvers;

public class IdadeResolver : IValueResolver<Funcionario, FuncionarioDTO, int>
{
    public int Resolve(Funcionario source, 
                       FuncionarioDTO destination, 
                       int destMember, 
                       ResolutionContext context)
    {
        var hoje = DateTime.Today;

        int idade = hoje.Year - source.Nascimento.Year;
        // Ajustando se ainda não fez aniversário

        if (source.Nascimento.Date > hoje.AddYears(-idade)) 
            idade--; 

        return idade;
    }
}
