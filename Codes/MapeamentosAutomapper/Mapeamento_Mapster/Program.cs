using Mapeamento_Mapster.DTOs;
using Mapeamento_Mapster.Entities;
using Mapeamento_Mapster.Mappings;
using Mapster;

Console.WriteLine("\nUsando o Mapster\n");

MapsterConfig.ConfigurarMapeamento();

var funcionarios = new List<Funcionario>
{
   new() { Id = 1, Nome = "Carlos Silva", Cargo = "Desenvolvedor .NET", Salario = 8500.50m,
           Endereco = new Endereco { Rua = "Rua A", Cidade = "São Paulo", Estado = "SP" }},
   new() { Id = 2, Nome = "Ana Souza", Cargo = "Analista de QA", Salario = 7000.00m,
           Endereco = new Endereco { Rua = "Rua B", Cidade = "Rio de Janeiro", Estado = "RJ" }},
   new() { Id = 3, Nome = "Roberto Lima", Cargo = "Gerente de Projetos", Salario = 12000.75m,  
           Endereco = new Endereco { Rua = "Rua C", Cidade = "Belo Horizonte", Estado = "MG" }}
};

var empresa = new Empresa
{
    Nome = "JcmTech Solutions",
    Funcionarios = funcionarios
};

EmpresaDTO empresaDTO = empresa.Adapt<EmpresaDTO>();

Console.WriteLine($"Empresa: {empresaDTO.Nome}");
Console.WriteLine("\nFuncionários:\n");

foreach (var f in empresaDTO.Funcionarios!)
{
    Console.WriteLine($"Nome: {f.Nome}");
    Console.WriteLine($"Cargo: {f.Cargo}");
    Console.WriteLine($"Salário: {f.SalarioFormatado}");
    Console.WriteLine($"Endereço: {f.EnderecoCompleto}");
    Console.WriteLine("----------------------------------------");
}
Console.ReadKey();

