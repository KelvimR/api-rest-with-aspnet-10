using api_rest_with_aspnet_10.Data.DTO.V1;

namespace api_rest_with_aspnet_10.Services;

public interface IBookService
{
    BookDTO Create(BookDTO book);
    BookDTO FindById(long Id);
    List<BookDTO> FindAll();
    BookDTO Update(BookDTO book);
    void Delete(long id);
}
