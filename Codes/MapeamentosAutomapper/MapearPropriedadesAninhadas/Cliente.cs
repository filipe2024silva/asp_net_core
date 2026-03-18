namespace MapearPropriedadesAninhadas;

public class Cliente
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public Endereco? EnderecoResidencial { get; set; }
    public Endereco? EnderecoComercial { get; set; }
}
