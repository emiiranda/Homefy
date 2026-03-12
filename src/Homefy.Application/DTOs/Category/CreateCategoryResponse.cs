using Homefy.Domain.Enums;

namespace Homefy.Application.DTOs.Category;

public sealed class CreateCategoryResponse
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public CategoryPurpose Purpose { get; init; }
}