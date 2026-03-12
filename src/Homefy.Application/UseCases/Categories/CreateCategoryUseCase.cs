using Homefy.Application.DTOs.Category;
using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Entities;

namespace Homefy.Application.UseCases.Categories;

public sealed class CreateCategoryUseCase(ICategoryRepository categoryRepository)
{
    private readonly ICategoryRepository _categoryRepository =
        categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<CreateCategoryResponse> ExecuteAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var category = new Category(request.Description, request.Purpose);

        await _categoryRepository.AddAsync(category, cancellationToken);

        return new CreateCategoryResponse
        {
            Id = category.Id,
            Description = category.Description,
            Purpose = category.Purpose
        };
    }
}