namespace Mapeamento_Profiles.Entities;

public class Funcionario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Cargo { get; set; }
    public decimal Salario { get; set; }
    public Departamento? Departamento { get; set; }
}
