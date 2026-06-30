using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api_rest_with_aspnet_10.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
//[EnableCors("LocalPolicy")] // Assim aplica o cors a todo o controller
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
    [ProducesResponseType(200, Type = typeof(List<PersonDTO>))]
    [ProducesResponseType(400)] // Bad Request: Utilizado quando a solicitação do cliente é inválida ou malformada.
    [ProducesResponseType(401)] // Unauthorized: Utilizado quando a autenticação é necessária e falhou ou ainda não foi fornecida.
    public IActionResult Get()
    {
        _logger.LogInformation("Getting all people");
        return Ok(_personService.FindAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(400)] 
    [ProducesResponseType(401)]
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
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    //[EnableCors("MultiplePolicy")] // Assim eu aplico somente a este endpoint, tenho granularidade isso configurado lá no config do cors
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
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
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
    [ProducesResponseType(204, Type = typeof(PersonDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deleting person with ID {id}", id);
        _personService.Delete(id);
        return NoContent();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(200, Type = typeof(PersonDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Disable(long id)
    {
        _logger.LogInformation("Disabling person with ID {id}", id);
        var disable = _personService.Disable(id);
        if(disable == null)
        {
            _logger.LogError("Failed to disable person with ID {id}", id);
            return NotFound();
        }

        _logger.LogDebug("Person with ID {id} disable successfully.", id);
        return Ok(disable);
    }
}

