using Mapeamento_ApiAspNetCore.Data.Context;
using Mapeamento_ApiAspNetCore.Entities;
using Mapeamento_ApiAspNetCore.Mappings.Profiles;
using Mapeamento_ApiAspNetCore.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddAutoMapper(typeof(ProdutoProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
        await SeedDatabase(dbContext);
    }
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
                  options.SwaggerEndpoint("/openapi/v1.json", "produtos api"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

static async Task SeedDatabase(AppDbContext context)
{
    if (!await context.Produtos.AnyAsync())
    {
        context.Produtos.AddRange(
            new Produto { Nome = "Notebook", Preco = 4500.00m, CodigoBarras = "NB123456", DataCriacao = DateTime.Now.AddDays(-45), Imposto = 10 },
            new Produto { Nome = "Smartphone", Preco = 2500.00m, CodigoBarras = "SP654321", DataCriacao = DateTime.Now.AddDays(-15), Imposto = 15 },
            new Produto { Nome = "Mouse", Preco = 20.00m, CodigoBarras = "MS987654", DataCriacao = DateTime.Now, Imposto = 5 }
        );
        await context.SaveChangesAsync();
    }
}