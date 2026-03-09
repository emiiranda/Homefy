using Homefy.Domain.Common;
using Homefy.Domain.Enums;
using Homefy.Domain.Exceptions;

namespace Homefy.Domain.Entities;

public class Category : EntityBase
{
    public const int MaxDescriptionLength = 400;

    public string Description { get; private set; } = string.Empty;
    public CategoryPurpose Purpose { get; private set; }

    private Category()
    {
    }

    public Category(string description, CategoryPurpose purpose)
    {
        SetDescription(description);
        SetPurpose(purpose);
    }

    public bool AllowsExpense()
    {
        return Purpose is CategoryPurpose.Expense or CategoryPurpose.Both;
    }

    public bool AllowsIncome()
    {
        return Purpose is CategoryPurpose.Income or CategoryPurpose.Both;
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Category description is required.");

        description = description.Trim();

        if (description.Length > MaxDescriptionLength)
            throw new DomainException($"Category description cannot exceed {MaxDescriptionLength} characters.");

        Description = description;
    }

    private void SetPurpose(CategoryPurpose purpose)
    {
        if (!Enum.IsDefined(purpose))
            throw new DomainException("Invalid category purpose.");

        Purpose = purpose;
    }
}