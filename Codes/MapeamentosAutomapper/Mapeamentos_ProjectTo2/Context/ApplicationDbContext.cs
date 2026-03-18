using Mapeamentos_ProjectTo2.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mapeamentos_ProjectTo2.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TesteDB.db");
    }
}
