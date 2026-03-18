namespace MapearPropriedadesAninhadas;

public class ClienteDTO
{
    public string? NomeCliente { get; set; }
    public string? EmailCliente { get; set; }
    public EnderecoDTO? EnderecoResidencialCliente { get; set; }
    public EnderecoDTO? EnderecoComercialCliente { get; set; }
}
