using Homefy.Application.DTOs.Transaction;
using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Entities;
using Homefy.Domain.Enums;
using Homefy.Domain.Exceptions;

namespace Homefy.Application.UseCases.Transactions;

public sealed class CreateTransactionUseCase(
    ITransactionRepository transactionRepository,
    IPersonRepository personRepository,
    ICategoryRepository categoryRepository)
{
    private readonly ITransactionRepository _transactionRepository =
        transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
    private readonly IPersonRepository _personRepository =
        personRepository ?? throw new ArgumentNullException(nameof(personRepository));
    private readonly ICategoryRepository _categoryRepository =
        categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public async Task<CreateTransactionResponse> ExecuteAsync(
        CreateTransactionRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var person = await _personRepository.GetByIdAsync(request.PersonId, cancellationToken)
            ?? throw new KeyNotFoundException($"Person with id '{request.PersonId}' was not found.");

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken)
            ?? throw new KeyNotFoundException($"Category with id '{request.CategoryId}' was not found.");

        // Menor de idade só pode registrar despesas
        if (person.IsMinor() && request.Type == TransactionType.Income)
            throw new DomainException("Minors can only register expense transactions.");

        // Finalidade da categoria deve ser compatível com o tipo da transação
        var purposeIsIncompatible =
            (request.Type == TransactionType.Expense && category.Purpose == CategoryPurpose.Income) ||
            (request.Type == TransactionType.Income && category.Purpose == CategoryPurpose.Expense);

        if (purposeIsIncompatible)
            throw new DomainException(
                $"Category purpose '{category.Purpose}' is not compatible with transaction type '{request.Type}'.");

        var transaction = new Transaction(
            request.Description,
            request.Amount,
            request.Type,
            request.CategoryId,
            request.PersonId);

        await _transactionRepository.AddAsync(transaction, cancellationToken);

        return new CreateTransactionResponse
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            Type = transaction.Type,
            CategoryId = transaction.CategoryId,
            PersonId = transaction.PersonId
        };
    }
}