using Mapeamentos_ProjectTo2.Context;
using Mapeamentos_ProjectTo2.Entities;
using Mapeamentos_ProjectTo2.Profiles;
using Mapeamentos_ProjectTo2.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;


Console.WriteLine("\nUsando ProjectTo<T> \n");

var serviceProvider = new ServiceCollection()
           .AddDbContext<ApplicationDbContext>()
           .AddAutoMapper(typeof(FuncionarioProfile))
           .AddScoped<FuncionarioService>()
           .BuildServiceProvider();

var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
var funcionarioService = serviceProvider.GetRequiredService<FuncionarioService>();

// Criar e popular banco com 100.000 registros
await InicializarBanco(context);

Console.WriteLine("\n🔹 Comparando Desempenho: ProjectTo<T> vs Map<T> 🔹");

// Medindo o tempo do ProjectTo<T>
var stopwatch = Stopwatch.StartNew();
var funcionariosDTO = await funcionarioService.ObterFuncionariosDTO();
stopwatch.Stop();
Console.WriteLine($"\n⏳ ProjectTo<T> executado em: {stopwatch.ElapsedMilliseconds} ms");

// Medindo o tempo do Map<T>
stopwatch.Restart();
var funcionariosMap = await funcionarioService.ObterFuncionariosMap();
stopwatch.Stop();
Console.WriteLine($"\n⏳ Map<T> executado em: {stopwatch.ElapsedMilliseconds} ms");


Console.ReadKey();


static async Task InicializarBanco(ApplicationDbContext context)
{
    await context.Database.EnsureDeletedAsync();
    await context.Database.EnsureCreatedAsync();

    var funcionarios = new List<Funcionario>();
    for (int i = 1; i <= 100000; i++)
    {
        funcionarios.Add(new Funcionario 
        { 
            Nome = $"Funcionario {i}", Cargo = "Desenvolvedor", Salario = 5000 
        });
    }

    context.Funcionarios.AddRange(funcionarios);
    await context.SaveChangesAsync();

    Console.WriteLine("✅ Banco de dados populado com 100.000 funcionários.");
}