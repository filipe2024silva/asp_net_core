namespace Mapeamentos_ConstructUsing.DTOs;

public class FuncionarioDTO
{
    public int Id { get; }
    public string NomeCompleto { get; }
    public decimal Salario { get; }
    public string Status { get; }

    // Construtor exigindo valores obrigatórios
    public FuncionarioDTO(int id, string nomeCompleto, decimal salario, string status = "Ativo")
    {
        Id = id;
        NomeCompleto = nomeCompleto;
        Salario = salario;
        Status = status;
    }
}
