using Homefy.Domain.Common;
using Homefy.Domain.Enums;
using Homefy.Domain.Exceptions;

namespace Homefy.Domain.Entities;

public sealed class Transaction : EntityBase
{
    public const int MaxDescriptionLength = 400;

    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid PersonId { get; private set; }

    public Category Category { get; private set; } = null!;
    public Person Person { get; private set; } = null!;

    private Transaction()
    {
    }

    public Transaction(
        string description,
        decimal amount,
        TransactionType type,
        Guid categoryId,
        Guid personId)
    {
        SetDescription(description);
        SetAmount(amount);
        SetType(type);
        SetCategoryId(categoryId);
        SetPersonId(personId);
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Transaction description is required.");

        description = description.Trim();

        if (description.Length > MaxDescriptionLength)
            throw new DomainException($"Transaction description cannot exceed {MaxDescriptionLength} characters.");

        Description = description;
    }

    private void SetAmount(decimal amount)
    {
        if (amount <= 0)
            throw new DomainException("Transaction amount must be greater than zero.");

        Amount = amount;
    }

    private void SetType(TransactionType type)
    {
        if (!Enum.IsDefined(type))
            throw new DomainException("Invalid transaction type.");

        Type = type;
    }

    private void SetCategoryId(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new DomainException("Category identifier is required.");

        CategoryId = categoryId;
    }

    private void SetPersonId(Guid personId)
    {
        if (personId == Guid.Empty)
            throw new DomainException("Person identifier is required.");

        PersonId = personId;
    }
}