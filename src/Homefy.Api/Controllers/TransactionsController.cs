using Homefy.Application.DTOs.Transaction;
using Homefy.Application.UseCases.Transactions;
using Homefy.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Homefy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TransactionsController(
    CreateTransactionUseCase createTransactionUseCase,
    GetAllTransactionsUseCase getAllTransactionsUseCase) : ControllerBase
{
    private readonly CreateTransactionUseCase _createTransactionUseCase = createTransactionUseCase;
    private readonly GetAllTransactionsUseCase _getAllTransactionsUseCase = getAllTransactionsUseCase;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _getAllTransactionsUseCase.ExecuteAsync(cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        [FromBody] CreateTransactionRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _createTransactionUseCase.ExecuteAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetAll), new { id = response.Id }, response);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}