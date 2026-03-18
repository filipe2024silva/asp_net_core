namespace Mapeamento_Mapster.Entities;

public class Funcionario
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Cargo { get; set; }
    public decimal Salario { get; set; }
    public Endereco? Endereco { get; set; }
}
