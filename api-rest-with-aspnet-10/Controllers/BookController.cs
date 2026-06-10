using api_rest_with_aspnet_10.Data.DTO.V1;
using api_rest_with_aspnet_10.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_rest_with_aspnet_10.Controllers;

[ApiController]
[Route("api/[controller]/v1")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BookController> _logger;
    public BookController(IBookService bookService, ILogger<BookController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }


    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Get()
    {
        _logger.LogInformation("Getting all books");
        return Ok(_bookService.FindAll());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(BookDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("Fething book with ID {id}", id);

        var book = _bookService.FindById(id);
        if (book == null)
        {
            _logger.LogWarning("Book with ID {id} not found", id);
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(BookDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Post([FromBody] BookDTO book)
    {
        _logger.LogInformation("Creating new person: {title}", book.Title);

        var createdBook = _bookService.Create(book);
        if (createdBook == null)
        {
            _logger.LogError("Failed to create book: {title}", book.Title);
            return NotFound();
        }

        //Aqui estamos adicionando os headers de depreciação para indicar que esta API está obsoleta e fornecer uma data de descontinuação.
        //Esses headers são úteis para informar os consumidores da API sobre a depreciação e permitir que eles se preparem para a transição para uma nova versão da API.
        Response.Headers.Add("X-API-Deprecated", "true");
        Response.Headers.Add("X-API-Deprecation-Date", "2026-12-31");

        return Ok(createdBook);
    }

    [HttpPut]
    [ProducesResponseType(200, Type = typeof(BookDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Update([FromBody] BookDTO book)
    {
        _logger.LogInformation("Updating book with ID {id}", book.Id);

        var createdBook = _bookService.Update(book);
        if (createdBook == null)
        {
            _logger.LogError("Failed to update book with ID {id}", book.Id);
            return NotFound();
        }

        _logger.LogDebug("Book update successfully: {title}", book.Title);
        return Ok(createdBook);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204, Type = typeof(BookDTO))]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public IActionResult Delete(int id)
    {
        _logger.LogInformation("Deleting book with ID {id}", id);
        _bookService.Delete(id);
        return NoContent();
    }
}
