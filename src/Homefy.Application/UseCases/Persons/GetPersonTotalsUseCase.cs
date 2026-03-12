using Homefy.Application.DTOs.Person;
using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Enums;

namespace Homefy.Application.UseCases.Persons;

public sealed class GetPersonTotalsUseCase(
    IPersonRepository personRepository,
    ITransactionRepository transactionRepository)
{
    private readonly IPersonRepository _personRepository =
        personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    private readonly ITransactionRepository _transactionRepository =
        transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));

    public async Task<GetPersonTotalsResponse> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var persons = await _personRepository.GetAllAsync(cancellationToken);
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);

        var personTotals = persons
            .Select(person =>
            {
                var personTransactions = transactions
                    .Where(t => t.PersonId == person.Id)
                    .ToList();

                var totalIncome = personTransactions
                    .Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Amount);

                var totalExpense = personTransactions
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                return new GetPersonTotalsItem
                {
                    Id = person.Id,
                    Name = person.Name,
                    Age = person.Age,
                    TotalIncome = totalIncome,
                    TotalExpense = totalExpense,
                    Balance = totalIncome - totalExpense
                };
            })
            .ToList();

        var grandTotalIncome = personTotals.Sum(p => p.TotalIncome);
        var grandTotalExpense = personTotals.Sum(p => p.TotalExpense);

        return new GetPersonTotalsResponse
        {
            Persons = personTotals,
            TotalIncome = grandTotalIncome,
            TotalExpense = grandTotalExpense,
            Balance = grandTotalIncome - grandTotalExpense
        };
    }
}