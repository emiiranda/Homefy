using Homefy.Domain.Entities;
using Homefy.Domain.Exceptions;

namespace Homefy.UnitTests.Domain.Entities;

public class PersonTests
{
    [Fact]
    public void Constructor_ShouldCreatePerson_WhenDataIsValid()
    {
        // Arrange
        var name = "Eduardo Miranda";
        var age = 30;

        // Act
        var person = new Person(name, age);

        // Assert
        Assert.NotEqual(Guid.Empty, person.Id);
        Assert.Equal(name, person.Name);
        Assert.Equal(age, person.Age);
    }

    [Fact]
    public void Constructor_ShouldTrimName_WhenNameHasExtraSpaces()
    {
        // Arrange
        var name = "  Eduardo Miranda  ";
        var age = 30;

        // Act
        var person = new Person(name, age);

        // Assert
        Assert.Equal("Eduardo Miranda", person.Name);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenNameIsEmpty()
    {
        // Arrange
        var name = "";
        var age = 30;

        // Act
        Person action() => new(name, age);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Person>)action);
        Assert.Equal("Person name is required.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenNameIsWhiteSpace()
    {
        // Arrange
        var name = "   ";
        var age = 30;

        // Act
        Person action() => new Person(name, age);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Person>)action);
        Assert.Equal("Person name is required.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenNameExceedsMaxLength()
    {
        // Arrange
        var name = new string('A', Person.MaxNameLength + 1);
        var age = 30;

        // Act
        Person action() => new(name, age);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Person>)action);
        Assert.Equal($"Person name cannot exceed {Person.MaxNameLength} characters.", exception.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowDomainException_WhenAgeIsNegative()
    {
        // Arrange
        var name = "Eduardo Miranda";
        var age = -1;

        // Act
        Person action() => new(name, age);

        // Assert
        var exception = Assert.Throws<DomainException>((Func<Person>)action);
        Assert.Equal("Person age cannot be negative.", exception.Message);
    }

    [Fact]
    public void IsMinor_ShouldReturnTrue_WhenAgeIsUnder18()
    {
        // Arrange
        var person = new Person("Eduardo Miranda", 17);

        // Act
        var result = person.IsMinor();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsMinor_ShouldReturnFalse_WhenAgeIs18OrMore()
    {
        // Arrange
        var person = new Person("Eduardo Miranda", 18);

        // Act
        var result = person.IsMinor();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Update_ShouldChangeNameAndAge_WhenDataIsValid()
    {
        // Arrange
        var person = new Person("Eduardo", 20);

        // Act
        person.Update("Eduardo Miranda", 21);

        // Assert
        Assert.Equal("Eduardo Miranda", person.Name);
        Assert.Equal(21, person.Age);
    }

    [Fact]
    public void Update_ShouldThrowDomainException_WhenNameIsInvalid()
    {
        // Arrange
        var person = new Person("Eduardo", 20);

        // Act
        void action() => person.Update("", 21);

        // Assert
        var exception = Assert.Throws<DomainException>(action);
        Assert.Equal("Person name is required.", exception.Message);
    }

    [Fact]
    public void Update_ShouldThrowDomainException_WhenAgeIsNegative()
    {
        // Arrange
        var person = new Person("Eduardo", 20);

        // Act
        void action() => person.Update("Eduardo Miranda", -1);

        // Assert
        var exception = Assert.Throws<DomainException>(action);
        Assert.Equal("Person age cannot be negative.", exception.Message);
    }
}