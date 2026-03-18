// Configuração do AutoMapper
using AutoMapper;
using Mapeamento_Resolvers.DTOs;
using Mapeamento_Resolvers.Entities;
using Mapeamento_Resolvers.Profiles;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Usando Resolvers\n");

var services = new ServiceCollection();
services.AddAutoMapper(typeof(FuncionarioProfile));
var serviceProvider = services.BuildServiceProvider();
var mapper = serviceProvider.GetRequiredService<IMapper>();

// Criando um funcionário de exemplo
var funcionario = new Funcionario
{
    Id = 1,
    Nome = "Amanda Siqueira",
    Salario = 7000m,
    Nascimento = new DateTime(2004, 8, 11)
};

// Mapeando para DTO
var funcionarioDTO = mapper.Map<FuncionarioDTO>(funcionario);

// Exibindo o resultado
Console.WriteLine($"ID: {funcionarioDTO.Id}");
Console.WriteLine($"Nome Completo: {funcionarioDTO.NomeCompleto}");
Console.WriteLine($"Salário Anual: {funcionarioDTO.SalarioAnual:C} por ano");
Console.WriteLine($"Idade: {funcionarioDTO.Idade} anos");

Console.ReadKey();

