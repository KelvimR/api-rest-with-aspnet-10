using api_rest_with_aspnet_10.Data.Converter.Contract;
using api_rest_with_aspnet_10.Data.DTO.V2;
using api_rest_with_aspnet_10.Models;

namespace api_rest_with_aspnet_10.Data.Converter.Implementations;

public class PersonConverter : IParser<Person, PersonDTO>
{
    // Aqui estamos convertendo um objeto do tipo PersonDTO para um objeto do tipo Person. Se o objeto de origem for nulo, retornamos nulo. Caso contrário, criamos um novo objeto Person e copiamos as propriedades correspondentes do PersonDTO.
    public Person Parse(PersonDTO origin)
    {
        if (origin == null) return null;
        return new Person
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address = origin.Address,
            Gender = origin.Gender
            //Birthday = origin.Birthday
        };
    }

    // Aqui estamos usando o LINQ para converter cada item da lista de PersonDTO para Person, utilizando o método Parse definido acima.
    public List<Person> ParseList(List<PersonDTO> origin)
    {
        if (origin == null) return null;
        return origin.Select(item => Parse(item)).ToList();
    }

    // Aqui estamos convertendo um objeto do tipo Person para um objeto do tipo PersonDTO. Se o objeto de origem for nulo, retornamos nulo. Caso contrário, criamos um novo objeto PersonDTO e copiamos as propriedades correspondentes do Person.
    public PersonDTO Parse(Person origin)
    {
        if (origin == null) return null;
        return new PersonDTO
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address = origin.Address,
            Gender = origin.Gender,
            Birthday = DateTime.Now //Como ainda não temos a propriedade Birthday na classe Person, estamos atribuindo a data atual para a propriedade Birthday do PersonDTO. Se a propriedade Birthday for adicionada posteriormente à classe Person, você pode simplesmente descomentar a linha acima e remover a atribuição de DateTime.Now.
            //Birthday = origin.Birthday ?? DateTime.Now
        };
    }

    //Aqui estamos usando o LINQ para converter cada item da lista de Person para PersonDTO, utilizando o método Parse definido acima.
    public List<PersonDTO> ParseList(List<Person> origin)
    {
        if (origin == null) return null;
        return origin.Select(item => Parse(item)).ToList();
    }
}
