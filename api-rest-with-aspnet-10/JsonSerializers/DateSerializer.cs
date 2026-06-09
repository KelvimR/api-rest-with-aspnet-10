using System.Text.Json;
using System.Text.Json.Serialization;

namespace api_rest_with_aspnet_10.JsonSerializers;

public class DateSerializer : JsonConverter<DateTime?>
{
    private readonly string _format = "dd/MM/yyyy";

    //Aqui estamos dizendo que quando o JSON for lido, ele deve ser convertido para um DateTime usando o formato "dd/MM/yyyy".
    //Se a conversão falhar, ele retorna null. 
    //Quando o JSON for escrito, ele verifica se o valor tem um valor (ou seja, não é null). Se tiver, ele escreve a data no formato especificado.
    //Caso contrário, ele escreve um valor nulo.
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if(DateTime.TryParseExact(reader.GetString(), _format, null, System.Globalization.DateTimeStyles.None, out DateTime date))
            return date;

        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStartArray(value.Value.ToString(_format));
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
