// Configuração do AutoMapper com o Profile
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Mapeamentos_ProjectTo.Context;
using Mapeamentos_ProjectTo.DTOs;
using Mapeamentos_ProjectTo.Entities;
using Mapeamentos_ProjectTo.Profiles;

Console.WriteLine("\nUsando ProjectTo<T>\n");
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<PessoaProfile>(); // Adiciona o Profile criado
});

var mapper = config.CreateMapper();

// Simulando um banco de dados com Entity Framework
using (var context = new AppDbContext())
{
    // Adicionando dados de exemplo
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    // Endereços
    var endereco1 = new Endereco { Rua = "Rua Exemplo 1", Cidade = "Santos", Estado = "SP" };
    var endereco2 = new Endereco { Rua = "Avenida Teste 2", Cidade = "Lins", Estado = "SP" };

    // Pessoas
    var pessoa1 = new Pessoa { Nome = "João Silva", Idade = 30, Endereco = endereco1 };
    var pessoa2 = new Pessoa { Nome = "Maria Oliveira", Idade = 25, Endereco = endereco2 };

    // Adicionando as pessoas ao contexto
    context.Pessoas.AddRange(pessoa1, pessoa2);
    context.SaveChanges();

    // Usando ProjectTo<T> para projetar a consulta
    var pessoasDTO = context.Pessoas
                            .ProjectTo<PessoaDTO>(config) 
                            .ToList();

    // Exibindo os resultados
    foreach (var p in pessoasDTO)
    {
        Console.WriteLine($"Nome: {p.Nome}, Idade: {p.Idade}, " +
                          $"\nEndereço: {p.EnderecoCompleto}\n");
    }
    Console.ReadKey();
}