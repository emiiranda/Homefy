using Homefy.Domain.Entities;
using Homefy.Domain.Enums;
using Homefy.Domain.Exceptions;

namespace Homefy.UnitTests.Domain.Entities;

public class TransactionTests
{
    [Fact]
    public void Constructor_ShouldCreateTransaction_WhenDataIsValid()
    {
        // Arrange
        var description = "Compra de supermercado";
        var amount = 120.50m;
        var type = TransactionType.Expense;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        var transaction = new Transaction(description, amount, type, categoryId, personId);

        // Assert
        Assert.NotEqual(Guid.Empty, transaction.Id);
        Assert.Equal(description, transaction.Description);
        Assert.Equal(amount, transaction.Amount);
        Assert.Equal(type, transaction.Type);
        Assert.Equal(categoryId, transaction.CategoryId);
        Assert.Equal(personId, transaction.PersonId);
    }

    [Fact]
    public void Constructor_ShouldTrimDescription_WhenDescriptionHasExtraSpaces()
    {
        // Arrange
        var description = "  Salário mensal  ";
        var amount = 5000m;
        var type = TransactionType.Income;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        var transaction = new Transaction(description, amount, type, categoryId, personId);

        // Assert
        Assert.Equal("Salário mensal", transaction.Description);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenDescriptionIsEmpty()
    {
        // Arrange
        var description = "";
        var amount = 200m;
        var type = TransactionType.Expense;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        Transaction action() => new(description, amount, type, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal("Transaction description is required.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenDescriptionIsWhitespace()
    {
        // Arrange
        var description = "   ";
        var amount = 100m;
        var type = TransactionType.Expense;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        Transaction action() => new(description, amount, type, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal("Transaction description is required.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenDescriptionExceedsMaxLength()
    {
        // Arrange
        var description = new string('A', Transaction.MaxDescriptionLength + 1);
        var amount = 100m;
        var type = TransactionType.Income;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        Transaction action() => new(description, amount, type, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal(
            $"Transaction description cannot exceed {Transaction.MaxDescriptionLength} characters.",
            exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenAmountIsZero()
    {
        // Arrange
        var description = "Serviço de limpeza";
        var amount = 0m;
        var type = TransactionType.Income;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        Transaction action() => new(description, amount, type, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal("Transaction amount must be greater than zero.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenAmountIsNegative()
    {
        // Arrange
        var description = "Compra não planejada";
        var amount = -50m;
        var type = TransactionType.Expense;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        Transaction action() => new(description, amount, type, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal("Transaction amount must be greater than zero.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenTypeIsInvalid()
    {
        // Arrange
        var description = "Aplicação ou investimento";
        var amount = 100m;
        var invalidType = (TransactionType)999;
        var categoryId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        Transaction action() => new(description, amount, invalidType, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal("Invalid transaction type.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenCategoryIdIsEmpty()
    {
        // Arrange
        var description = "Compra de roupas";
        var amount = 250m;
        var categoryId = Guid.Empty;
        var personId = Guid.NewGuid();

        // Act
        Transaction action() => new(description, amount, TransactionType.Expense, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal("Category identifier is required.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenPersonIdIsEmpty()
    {
        // Arrange
        var description = "Venda de produto";
        var amount = 200m;
        var categoryId = Guid.NewGuid();
        var personId = Guid.Empty;

        // Act
        Transaction action() => new(description, amount, TransactionType.Income, categoryId, personId);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Transaction>)action);
        Assert.Equal("Person identifier is required.", exception.Message);
    }
}