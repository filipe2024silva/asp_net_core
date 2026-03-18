using AutoMapper;

namespace Mapeamento_ApiAspNetCore.Mappings.Converters;

public class CodigoBarrasConverter : IValueConverter<string?, string?>
{
    public string? Convert(string? source, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source))
        {
            return "Código de barras não disponível.";
        }
        else
        {
            return $"CB-{source.Substring(0, 4)}-{source.Substring(4)}";
        }
    }
}
