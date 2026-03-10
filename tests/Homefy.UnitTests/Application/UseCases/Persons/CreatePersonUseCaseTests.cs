using Homefy.Application.UseCases.Persons;
using Homefy.Domain.Exceptions;
using Homefy.Application.DTOs.Person;
using Homefy.UnitTests.Application.UseCases.Persons.TestSupport;

namespace Homefy.UnitTests.Application.UseCases.Persons;

public sealed class CreatePersonUseCaseTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        // Act
        static CreatePersonUseCase act() => new(null!);

        // Assert
        var exception = Assert.Throws<ArgumentNullException>((Func<CreatePersonUseCase>)act);
        Assert.Equal("personRepository", exception.ParamName);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCreatePersonAndReturnResponse()
    {
        // Arrange
        var repository = new FakePersonRepository();
        var useCase = new CreatePersonUseCase(repository);
        var request = PersonTestData.CreateValidRequest();

        // Act
        var response = await useCase.ExecuteAsync(request, TestContext.Current.CancellationToken);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(Guid.Empty, response.Id);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Age, response.Age);

        Assert.Equal(1, repository.AddAsyncCallCount);
        Assert.NotNull(repository.AddedPerson);
        Assert.Equal(request.Name, repository.AddedPerson!.Name);
        Assert.Equal(request.Age, repository.AddedPerson.Age);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowArgumentNullException_WhenRequestIsNull()
    {
        // Arrange
        var repository = new FakePersonRepository();
        var useCase = new CreatePersonUseCase(repository);

        // Act
        Task<CreatePersonResponse> act() => useCase.ExecuteAsync(null!);

        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentNullException>((Func<Task<CreatePersonResponse>>)act);
        Assert.Equal("request", exception.ParamName);
        Assert.Equal(0, repository.AddAsyncCallCount);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowDomainException_WhenNameIsInvalid()
    {
        // Arrange
        var repository = new FakePersonRepository();
        var useCase = new CreatePersonUseCase(repository);
        var request = PersonTestData.CreateRequestWithInvalidName();

        // Act
        Task<CreatePersonResponse> act() => useCase.ExecuteAsync(request);

        // Assert
        await Assert.ThrowsAsync<DomainException>((Func<Task<CreatePersonResponse>>)act);
        Assert.Equal(0, repository.AddAsyncCallCount);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowDomainException_WhenAgeIsInvalid()
    {
        // Arrange
        var repository = new FakePersonRepository();
        var useCase = new CreatePersonUseCase(repository);
        var request = PersonTestData.CreateRequestWithInvalidAge();

        // Act
        Task<CreatePersonResponse> act() => useCase.ExecuteAsync(request);

        // Assert
        await Assert.ThrowsAsync<DomainException>((Func<Task<CreatePersonResponse>>)act);
        Assert.Equal(0, repository.AddAsyncCallCount);
    }
}