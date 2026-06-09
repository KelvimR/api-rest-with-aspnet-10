using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Models;
using api_rest_with_aspnet_10.Repositories;
using Mapster;

namespace api_rest_with_aspnet_10.Services.Implementations;

public class BookServicesImpl : IBookService
{
    private readonly IRepository<Book> _repository;
    public BookServicesImpl(IRepository<Book> repository)
    {
        _repository = repository;
    }

    //Quando não precisamos fazer nenhuma customizacao, o ideal é utilizar uma ferramenta como o Mapster.
    // Aqui estamos utilizando o Mapster para converter a lista de objetos retornada pelo repositório para uma lista de BookDTO
    public List<BookDTO> FindAll()
    {
        return _repository.FindAll().Adapt<List<BookDTO>>();
    }

    // Aqui estamos utilizando o Mapster para converter o objeto retornado pelo repositório para um BookDTO
    // Para isso utilizamos o método Adapt, que é uma extensão do Mapster, para fazer a conversão de forma simples e eficiente.
    public BookDTO FindById(long Id)
    {
        return _repository.FindById(Id).Adapt<BookDTO>();
    }

    // No Create/Update estamos utilizando o Mapster para converter o objeto BookDTO recebido como parâmetro para um objeto Book, que é a entidade que será persistida no banco de dados.
    // Depois de criar a entidade, utilizamos o repositório para salvar a entidade no banco de dados e, em seguida, retornamos o objeto BookDTO correspondente à entidade criada.
    public BookDTO Create(BookDTO book)
    {
        var entity = book.Adapt<Book>();
        entity = _repository.Create(entity);
        return _repository.Adapt<BookDTO>();
    }

    public BookDTO Update(BookDTO book)
    {
        var entity = book.Adapt<Book>();
        entity = _repository.Update(entity);
        return _repository.Adapt<BookDTO>();
    }
    public void Delete(long id)
    {
        _repository.Delete(id);
    }
}
