using AutoMapper;
using Mapeamentos_ConstructUsing.DTOs;
using Mapeamentos_ConstructUsing.Entities;
using Mapeamentos_ConstructUsing.Profiles;

Console.WriteLine("\nUsando ConstructUsing\n");
// Configurando o AutoMapper
var config = new MapperConfiguration(cfg => cfg.AddProfile<FuncionarioProfile>());
var mapper = config.CreateMapper();

// Criando um funcionário
var funcionario = new Funcionario
{
    Id = 1, Nome = "Anita Gomes", Salario = 5000.00m,
    DataAdmissao = new DateTime(2022, 8, 10)
};

// Mapeando para DTO
var funcionarioDTO = mapper.Map<FuncionarioDTO>(funcionario);

// Exibindo os resultados
Console.WriteLine("\n---- FuncionarioDTO ----\n");
Console.WriteLine($"Id: {funcionarioDTO.Id}");
Console.WriteLine($"Nome Completo: {funcionarioDTO.NomeCompleto}");
Console.WriteLine($"Salário: {funcionarioDTO.Salario}");
Console.WriteLine($"Status: {funcionarioDTO.Status}");
Console.ReadKey();