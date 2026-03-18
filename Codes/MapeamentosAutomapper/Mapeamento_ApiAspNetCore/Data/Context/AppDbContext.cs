using Mapeamento_ApiAspNetCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mapeamento_ApiAspNetCore.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>()
            .Property(p => p.Preco)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Produto>()
            .Property(p => p.Imposto)
            .HasPrecision(18, 2);
    }
}
