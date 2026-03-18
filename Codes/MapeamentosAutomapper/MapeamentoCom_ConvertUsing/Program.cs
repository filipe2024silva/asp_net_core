using AutoMapper;
using MapeamentoCom_ConvertUsing;

// Configuração do AutoMapper
var config = new MapperConfiguration(cfg => cfg.CreateMap<Origem, Destino>()
  .ForMember(dest => dest.DataNascimento, opt => 
           opt.ConvertUsing<StringToDateTimeValueConverter, string>()));

var mapper = config.CreateMapper();

// Dados de origem
var origem = new Origem { Nome = "João da Silva", 
                          DataNascimento = "15-03-1990" };

// Mapeamento
var destino = mapper.Map<Destino>(origem);

// Exibição dos resultados
Console.WriteLine($"Nome: {destino.Nome}");
Console.WriteLine($"Data de Nascimento: {destino.DataNascimento}");

Console.ReadKey();

// Conversor de valor
public class StringToDateTimeValueConverter : IValueConverter<string, DateTime>
{
    public DateTime Convert(string source, ResolutionContext context)
    {
        if (DateTime.TryParseExact(source, "dd-MM-yyyy", null, 
            System.Globalization.DateTimeStyles.None, out DateTime data))
        {
            return data;
        }
        else
        {
            throw new FormatException("Formato de data inválido.");
        }
    }
}