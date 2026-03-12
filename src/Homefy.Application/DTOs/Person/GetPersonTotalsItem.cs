namespace Homefy.Application.DTOs.Person;

public sealed class GetPersonTotalsItem
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Age { get; init; }
    public decimal TotalIncome { get; init; }
    public decimal TotalExpense { get; init; }
    public decimal Balance { get; init; }
}