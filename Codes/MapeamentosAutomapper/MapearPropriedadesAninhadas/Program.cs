using AutoMapper;
using MapearPropriedadesAninhadas;

var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Endereco, EnderecoDTO>()
        .ForMember(dest => dest.Rua, opt => opt.MapFrom(src => src.Logradouro))
        .ForMember(dest => dest.NumeroCasa, opt => opt.MapFrom(src => src.Numero))
        .ForMember(dest => dest.ComplementoEndereco, opt => opt.MapFrom(src => src.Complemento))
        .ForMember(dest => dest.BairroEndereco, opt => opt.MapFrom(src => src.Bairro))
        .ForMember(dest => dest.CidadeEndereco, opt => opt.MapFrom(src => src.Cidade))
        .ForMember(dest => dest.UfEndereco, opt => opt.MapFrom(src => src.UF))
        .ForMember(dest => dest.CepEndereco, opt => opt.MapFrom(src => src.CEP));

    cfg.CreateMap<Cliente, ClienteDTO>()
        .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Nome))
        .ForMember(dest => dest.EmailCliente, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.EnderecoResidencialCliente, opt => opt.MapFrom(src => src.EnderecoResidencial))
        .ForMember(dest => dest.EnderecoComercialCliente, opt => opt.MapFrom(src => src.EnderecoComercial));
});

// Criar um objeto de origem
var clienteOrigem = new Cliente
{
    Id = 1,
    Nome = "Maria Silva", Email = "maria@email.com", EnderecoResidencial = new Endereco
    {
        Id = 1,
        Logradouro = "Av. Brasil",
        Numero = "1000",
        Complemento = "Apto 10",
        Bairro = "Centro",
        Cidade = "São Paulo",
        UF = "SP",
        CEP = "01000-000"
    },
    EnderecoComercial = new Endereco
    {
        Id = 2,
        Logradouro = "Rua Augusta",
        Numero = "123",
        Complemento = "",
        Bairro = "Consolação",
        Cidade = "São Paulo",
        UF = "SP",
        CEP = "01305-000"
    }
};

//cria a instancia do AutoMapper (IMapper)
var mapper = config.CreateMapper();

// Realizar o mapeamento
var clienteDTO = mapper.Map<ClienteDTO>(clienteOrigem);

// Exibir o resultado
Console.WriteLine($"Nome Cliente: {clienteDTO.NomeCliente}");
Console.WriteLine($"Email Cliente: {clienteDTO.EmailCliente}");
Console.WriteLine($"Endereço Residencial: {clienteDTO.EnderecoResidencialCliente?.Rua}, {clienteDTO.EnderecoResidencialCliente?.NumeroCasa} - {clienteDTO.EnderecoResidencialCliente?.CepEndereco}");
Console.WriteLine($"Endereço Comercial: {clienteDTO.EnderecoComercialCliente?.Rua}, {clienteDTO.EnderecoComercialCliente?.NumeroCasa} - {clienteDTO.EnderecoComercialCliente?.CepEndereco}");

Console.ReadKey();