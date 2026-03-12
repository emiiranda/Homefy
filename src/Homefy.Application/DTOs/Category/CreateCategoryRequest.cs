using Homefy.Domain.Enums;

namespace Homefy.Application.DTOs.Category;

public sealed class CreateCategoryRequest
{
    public string Description { get; init; } = string.Empty;
    public CategoryPurpose Purpose { get; init; }
}