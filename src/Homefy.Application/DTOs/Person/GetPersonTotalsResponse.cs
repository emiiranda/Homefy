namespace Homefy.Application.DTOs.Person;

public sealed class GetPersonTotalsResponse
{
    public List<GetPersonTotalsItem> Persons { get; init; } = [];
    public decimal TotalIncome { get; init; }
    public decimal TotalExpense { get; init; }
    public decimal Balance { get; init; }
}