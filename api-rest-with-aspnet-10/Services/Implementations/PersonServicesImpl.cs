using api_rest_with_aspnet_10.Data.Converter.Implementations;
using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Models;
using api_rest_with_aspnet_10.Repositories;
using Mapster;
using System.ComponentModel;

namespace api_rest_with_aspnet_10.Services.Implementations;

public class PersonServicesImpl : IPersonService
{
    private IRepository<Person> _repository;
    private readonly PersonConverter _converter;

    public PersonServicesImpl(IRepository<Person> repository)
    {
        _repository = repository;
        _converter = new PersonConverter(); // Não foi atráves de injeção de dependência porque o converter não tem dependências, ou seja, é uma classe simples que pode ser instanciada diretamente.
    }

    public PersonDTO FindById(long Id)
    {
        return _repository.FindById(Id).Adapt<PersonDTO>();
    }

    // O método FindAll retorna uma lista de entidades Person, que é convertida para uma lista de DTOs PersonDTO usando o método ParseList do converter.
    public List<PersonDTO> FindAll()
    {
        return _repository.FindAll().Adapt<List<PersonDTO>>();
    }

    // O método Create/Update recebe um DTO PersonDTO, converte para a entidade Person usando o método Parse do converter, chama o método Create do repositório para salvar a entidade no banco de dados, e depois converte a entidade salva de volta para um DTO PersonDTO para retornar ao cliente.
    public PersonDTO Create(PersonDTO person)
    {
        var entity = person.Adapt<Person>();
        entity = _repository.Create(entity);
        return entity.Adapt<PersonDTO>();
    }

    public PersonDTO Update(PersonDTO person)
    {
        var entity = person.Adapt<Person>();
        entity = _repository.Update(entity);
        return entity.Adapt<PersonDTO>();
    }

    public void Delete(long id)
    {
        _repository.Delete(id);
    }
}
