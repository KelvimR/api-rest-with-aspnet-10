using api_rest_with_aspnet_10.JsonSerializers;
using System.Text.Json.Serialization;

namespace api_rest_with_aspnet_10.Data.DTO.V1;

public class PersonDTO
{
    //Aplicamos esse formato quando um cliente tem uma demanda especifica de como o JSON deve ser formatado, ou seja, quando o cliente tem um contrato específico a ser seguido.
    //[JsonPropertyName("code")]
    public long Id { get; set; }
    //[JsonPropertyName("fist_Name")]
    public string FirstName { get; set; }
    //[JsonPropertyName("last_Name")]
    public string LastName { get; set; }
    //[JsonPropertyOrder(1)] //Essa propriedade indica que este será apresentado primeiro na lista, consigo serializar em ordem
    public string Address { get; set; }
    //[JsonConverter(typeof(GenderSerializer))] // Aqui estamos customizando a serialização e desserialização da propriedade
    public string Gender { get; set; }
    //[JsonConverter(typeof(DateSerializer))] //Aqui estamos dizendo que a propriedade BirthDate deve ser serializada e desserializada usando o DateSerializer personalizado que criamos.
    public DateTime? BirthDate { get; set; }
}
