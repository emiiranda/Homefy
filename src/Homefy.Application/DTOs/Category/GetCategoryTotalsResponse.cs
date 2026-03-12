namespace Homefy.Application.DTOs.Category;

public sealed class GetCategoryTotalsResponse
{
    public List<GetCategoryTotalsItem> Categories { get; init; } = []; // ✅ tipo atualizado
    public decimal TotalIncome { get; init; }
    public decimal TotalExpense { get; init; }
    public decimal Balance { get; init; }
}