using AutoMapper;
using Mapeamento_Profiles.DTOs;
using Mapeamento_Profiles.Entities;
using Mapeamento_Profiles.Profiles;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Usando Resolvers\n");

// Configuração do AutoMapper
var serviceProvider = new ServiceCollection()
    .AddAutoMapper(typeof(FuncionarioProfile))
    .BuildServiceProvider();

var mapper = serviceProvider.GetRequiredService<IMapper>();

// Criando um funcionário de exemplo
var funcionario = new Funcionario
{
    Id = 1,
    Nome = "Carlos Silva",
    Salario = 5000m,
    Nascimento = new DateTime(1990, 5, 15)
};

// Mapeando para DTO
var funcionarioDTO = mapper.Map<FuncionarioDTO>(funcionario);

// Exibindo o resultado
Console.WriteLine($"ID: {funcionarioDTO.Id}");
Console.WriteLine($"Nome Completo: {funcionarioDTO.NomeCompleto}");
Console.WriteLine($"Salário Anual: {funcionarioDTO.SalarioAnual}");
Console.WriteLine($"Idade: {funcionarioDTO.Idade}");

Console.ReadKey();