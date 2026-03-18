namespace Mapeamento_Profiles.DTOs;

public class FuncionarioDTO
{
    public int Id { get; set; }
    public string? NomeCompleto { get; set; }
    public string? CargoAtual { get; set; }
    public decimal SalarioAnual { get; set; }
    public DepartamentoDTO? Departamento { get; set; }
}
