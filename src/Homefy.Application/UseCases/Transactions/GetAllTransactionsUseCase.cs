using Homefy.Application.DTOs.Transaction;
using Homefy.Application.Interfaces.Repositories;

namespace Homefy.Application.UseCases.Transactions;

public sealed class GetAllTransactionsUseCase(ITransactionRepository transactionRepository)
{
    private readonly ITransactionRepository _transactionRepository =
        transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));

    public async Task<List<GetAllTransactionsResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);

        return transactions
            .Select(transaction => new GetAllTransactionsResponse
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Type = transaction.Type,
                CategoryId = transaction.CategoryId,
                CategoryDescription = transaction.Category.Description,
                PersonId = transaction.PersonId,
                PersonName = transaction.Person.Name
            })
            .ToList();
    }
}