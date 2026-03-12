using Homefy.Domain.Enums;

namespace Homefy.Application.DTOs.Category;

public sealed class GetCategoryTotalsItem
{
    public Guid Id { get; init; }
    public string Description { get; init; } = string.Empty;
    public CategoryPurpose Purpose { get; init; }
    public decimal TotalIncome { get; init; }
    public decimal TotalExpense { get; init; }
    public decimal Balance { get; init; }
}