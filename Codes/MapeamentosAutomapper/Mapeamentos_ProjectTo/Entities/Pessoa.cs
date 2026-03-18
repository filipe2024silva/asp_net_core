namespace Mapeamentos_ProjectTo.Entities;

public class Pessoa
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public int Idade { get; set; }
    public int EnderecoId { get; set; }
    // Relacionamento com Endereco
    public Endereco? Endereco { get; set; }
}
