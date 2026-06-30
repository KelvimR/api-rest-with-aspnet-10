using api_rest_with_aspnet_10.Data.DTO.V1;

namespace api_rest_with_aspnet_10.Services;

public interface IPersonService
{
    PersonDTO Create(PersonDTO person);
    PersonDTO FindById(long Id);
    List<PersonDTO> FindAll();
    PersonDTO Update(PersonDTO person);
    void Delete(long id);

    PersonDTO Disable(long id);

}
