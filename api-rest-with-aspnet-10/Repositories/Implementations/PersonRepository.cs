using api_rest_with_aspnet_10.Context;
using api_rest_with_aspnet_10.Models;

namespace api_rest_with_aspnet_10.Repositories.Implementations;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{

    public PersonRepository(MSSQLContext context) : base(context) { }
    
    public Person Disable(long id)
    {
        var person = _context.Persons.Find(id);
        if (person == null) return null;
        person.Enabled = false;
        _context.SaveChanges();
        return person;
    }
}
