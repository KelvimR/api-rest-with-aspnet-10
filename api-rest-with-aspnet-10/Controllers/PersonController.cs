using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_rest_with_aspnet_10.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;

    public PersonController(IPersonService personService, ILogger<PersonController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Getting all people");
        return Ok(_personService.FindAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        _logger.LogInformation("Fething person with ID {id}", id);

        var person = _personService.FindById(id);
        if (person == null)
        {
            _logger.LogWarning("Person with ID {id} not found", id);
            return NotFound();
        }
        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] PersonDTO person)
    {
        _logger.LogInformation("Creating new person: {fistName}", person.FirstName);

        var createdPerson = _personService.Create(person);
        if (createdPerson == null)
        {
            _logger.LogError("Failed to create person: {fistName}", person.FirstName);
            return NotFound();
        }
        return Ok(createdPerson);
    }

    [HttpPut]
    public IActionResult Update([FromBody] PersonDTO person)
    {
        _logger.LogInformation("Updating person with ID {id}", person.Id);

        var createdPerson = _personService.Update(person);
        if (createdPerson == null)
        {
            _logger.LogError("Failed to update person with ID {id}", person.Id);
            return NotFound();
        }

        _logger.LogDebug("Person update successfully: {fistName}", person.Id);
        return Ok(createdPerson);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deleting person with ID {id}", id);
        _personService.Delete(id);
        return NoContent();
    }
}

