using Homefy.Application.DTOs.Category;
using Homefy.Application.Interfaces.Repositories;

namespace Homefy.Application.UseCases.Categories;

public sealed class GetAllCategoriesUseCase(ICategoryRepository categoryRepository)
{
    private readonly ICategoryRepository _categoryRepository =
        categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<List<GetAllCategoriesResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);

        return categories
            .Select(category => new GetAllCategoriesResponse
            {
                Id = category.Id,
                Description = category.Description,
                Purpose = category.Purpose
            })
            .ToList();
    }
}