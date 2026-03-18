using Mapeamentos_ProjectTo.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mapeamentos_ProjectTo.Context;

// Contexto do Entity Framework
public class AppDbContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TesteDB.db");
    }
}
