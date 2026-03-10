using Homefy.Application.DTOs.Person;
using Homefy.Application.UseCases.Persons;
using Microsoft.AspNetCore.Mvc;

namespace Homefy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PersonsController(
    CreatePersonUseCase createPersonUseCase,
    GetAllPersonsUseCase getAllPersonsUseCase,
    GetPersonByIdUseCase getPersonByIdUseCase,
    UpdatePersonUseCase updatePersonUseCase,
    DeletePersonUseCase deletePersonUseCase) : ControllerBase
{
    private readonly CreatePersonUseCase _createPersonUseCase = createPersonUseCase;
    private readonly GetAllPersonsUseCase _getAllPersonsUseCase = getAllPersonsUseCase;
    private readonly GetPersonByIdUseCase _getPersonByIdUseCase = getPersonByIdUseCase;
    private readonly UpdatePersonUseCase _updatePersonUseCase = updatePersonUseCase;
    private readonly DeletePersonUseCase _deletePersonUseCase = deletePersonUseCase;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _getAllPersonsUseCase.ExecuteAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _getPersonByIdUseCase.ExecuteAsync(id, cancellationToken);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreatePersonRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _createPersonUseCase.ExecuteAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = response.Id },
            response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdatePersonBodyRequest request,
        CancellationToken cancellationToken)
    {
        var useCaseRequest = new UpdatePersonRequest
        {
            Id = id,
            Name = request.Name,
            Age = request.Age
        };

        try
        {
            var response = await _updatePersonUseCase.ExecuteAsync(useCaseRequest, cancellationToken);
            return Ok(response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _deletePersonUseCase.ExecuteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}