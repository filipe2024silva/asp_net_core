using Mapeamento_Mapster.DTOs;
using Mapeamento_Mapster.Entities;
using Mapster;

namespace Mapeamento_Mapster.Mappings;

public static class MapsterConfig
{
    public static void ConfigurarMapeamento()
    {
        TypeAdapterConfig<Funcionario, FuncionarioDTO>
            .NewConfig()
            .Map(dest => dest.SalarioFormatado, src => $"R$ {src.Salario:N2}")
            .Map(dest => dest.EnderecoCompleto, src => $"{src.Endereco!.Rua}, {src.Endereco.Cidade} - {src.Endereco.Estado}");

        TypeAdapterConfig<Empresa, EmpresaDTO>
            .NewConfig()
            .Map(dest => dest.Funcionarios, src => src.Funcionarios.Adapt<List<FuncionarioDTO>>());
    }
}
