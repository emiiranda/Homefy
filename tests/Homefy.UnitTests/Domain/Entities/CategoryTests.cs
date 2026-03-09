using Homefy.Domain.Entities;
using Homefy.Domain.Enums;
using Homefy.Domain.Exceptions;

namespace Homefy.UnitTests.Domain.Entities;

public class CategoryTests
{
    [Fact]
    public void Constructor_ShouldCreateCategory_WhenDataIsValid()
    {
        // Arrange
        var description = "Alimentação";
        var purpose = CategoryPurpose.Expense;

        // Act
        var category = new Category(description, purpose);

        // Assert
        Assert.NotEqual(Guid.Empty, category.Id);
        Assert.Equal(description, category.Description);
        Assert.Equal(purpose, category.Purpose);
    }

    [Fact]
    public void Constructor_ShouldTrimDescription_WhenDescriptionHasExtraSpaces()
    {
        // Arrange
        var description = "  Transporte  ";
        var purpose = CategoryPurpose.Expense;

        // Act
        var category = new Category(description, purpose);

        // Assert
        Assert.Equal("Transporte", category.Description);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenDescriptionIsEmpty()
    {
        // Arrange
        var description = "";
        var purpose = CategoryPurpose.Income;

        // Act
        Category action() => new(description, purpose);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Category>)action);
        Assert.Equal("Category description is required.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenDescriptionIsWhitespace()
    {
        // Arrange
        var description = "   ";
        var purpose = CategoryPurpose.Income;

        // Act
        Category action() => new(description, purpose);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Category>)action);
        Assert.Equal("Category description is required.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenDescriptionExceedsMaxLength()
    {
        // Arrange
        var description = new string('A', Category.MaxDescriptionLength + 1);
        var purpose = CategoryPurpose.Expense;

        // Act
        Category action() => new(description, purpose);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Category>)action);
        Assert.Equal(
            $"Category description cannot exceed {Category.MaxDescriptionLength} characters.",
            exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenPurposeIsInvalid()
    {
        // Arrange
        var description = "Educação";
        var invalidPurpose = (CategoryPurpose)999;

        // Act
        Category action() => new(description, invalidPurpose);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Category>)action);
        Assert.Equal("Invalid category purpose.", exception.Message);
    }

    [Theory]
    [InlineData(CategoryPurpose.Expense, true, false)]
    [InlineData(CategoryPurpose.Income, false, true)]
    [InlineData(CategoryPurpose.Both, true, true)]
    public void AllowsExpenseAndIncome_ShouldReturnCorrectResultBasedOnPurpose(
        CategoryPurpose purpose,
        bool allowsExpense,
        bool allowsIncome)
    {
        // Arrange
        var category = new Category("Teste", purpose);

        // Act
        var expenseResult = category.AllowsExpense();
        var incomeResult = category.AllowsIncome();

        // Assert
        Assert.Equal(allowsExpense, expenseResult);
        Assert.Equal(allowsIncome, incomeResult);
    }
}