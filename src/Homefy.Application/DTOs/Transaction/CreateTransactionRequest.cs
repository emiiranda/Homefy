using Homefy.Domain.Enums;

namespace Homefy.Application.DTOs.Transaction;

public sealed class CreateTransactionRequest
{
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public TransactionType Type { get; init; }
    public Guid CategoryId { get; init; }
    public Guid PersonId { get; init; }
}