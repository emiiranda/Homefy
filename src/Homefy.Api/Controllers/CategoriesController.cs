using Homefy.Application.DTOs.Category;
using Homefy.Application.UseCases.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Homefy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CategoriesController(
    CreateCategoryUseCase createCategoryUseCase,
    GetAllCategoriesUseCase getAllCategoriesUseCase,
    GetCategoryTotalsUseCase getCategoryTotalsUseCase) : ControllerBase
{
    private readonly CreateCategoryUseCase _createCategoryUseCase = createCategoryUseCase;
    private readonly GetAllCategoriesUseCase _getAllCategoriesUseCase = getAllCategoriesUseCase;
    private readonly GetCategoryTotalsUseCase _getCategoryTotalsUseCase = getCategoryTotalsUseCase;

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _getAllCategoriesUseCase.ExecuteAsync(cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _createCategoryUseCase.ExecuteAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetAll),
            new { id = response.Id },
            response);
    }

    [HttpGet("totals")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotals(CancellationToken cancellationToken)
    {
        var response = await _getCategoryTotalsUseCase.ExecuteAsync(cancellationToken);
        return Ok(response);
    }
}