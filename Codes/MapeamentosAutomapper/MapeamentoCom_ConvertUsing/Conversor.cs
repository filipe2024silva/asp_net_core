namespace MapeamentoCom_ConvertUsing;

public static class Conversor
{
    public static DateTime DataNascimento(string dataString)
    {
        // Lógica de conversão da string para DateTime
        if (DateTime.TryParseExact(dataString, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data))
        {
            return data;
        }
        else
        {
            // Lidar com erros de conversão (lançar exceção, retornar valor padrão, etc.)
            throw new FormatException("Formato de data inválido.");
        }
    }

}

