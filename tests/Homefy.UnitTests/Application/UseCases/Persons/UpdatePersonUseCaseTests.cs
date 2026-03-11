using Homefy.Application.DTOs.Person;
using Homefy.Application.UseCases.Persons;
using Homefy.Domain.Exceptions;
using Homefy.UnitTests.Application.UseCases.Persons.TestSupport;

namespace Homefy.UnitTests.Application.UseCases.Persons;

public sealed class UpdatePersonUseCaseTests
{
    private static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        static UpdatePersonUseCase act() => new(null!);

        var exception = Assert.Throws<ArgumentNullException>((Func<UpdatePersonUseCase>)act);
        Assert.Equal("personRepository", exception.ParamName);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowArgumentNullException_WhenRequestIsNull()
    {
        var repository = new FakePersonRepository();
        var useCase = new UpdatePersonUseCase(repository);

        Task<UpdatePersonResponse> act() => useCase.ExecuteAsync(null!, CancellationToken);

        var exception = await Assert.ThrowsAsync<ArgumentNullException>((Func<Task<UpdatePersonResponse>>)act);
        Assert.Equal("request", exception.ParamName);
        Assert.Equal(0, repository.UpdateAsyncCallCount);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
    {
        var repository = new FakePersonRepository();
        var useCase = new UpdatePersonUseCase(repository);

        var request = new UpdatePersonRequest
        {
            Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
            Name = "Updated Name",
            Age = 35
        };

        Task<UpdatePersonResponse> act() => useCase.ExecuteAsync(request, CancellationToken);

        await Assert.ThrowsAsync<KeyNotFoundException>((Func<Task<UpdatePersonResponse>>)act);
        Assert.Equal(1, repository.GetByIdAsyncCallCount);
        Assert.Equal(0, repository.UpdateAsyncCallCount);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowDomainException_WhenDataIsInvalid()
    {
        var repository = new FakePersonRepository();
        repository.SetPersons(PersonTestData.CreatePersons());

        var useCase = new UpdatePersonUseCase(repository);

        var request = new UpdatePersonRequest
        {
            Id = PersonTestData.Person1Id,
            Name = string.Empty,
            Age = 30
        };

        Task<UpdatePersonResponse> act() => useCase.ExecuteAsync(request, CancellationToken);

        await Assert.ThrowsAsync<DomainException>((Func<Task<UpdatePersonResponse>>)act);
        Assert.Equal(1, repository.GetByIdAsyncCallCount);
        Assert.Equal(0, repository.UpdateAsyncCallCount);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldUpdatePersonAndReturnResponse()
    {
        var expectedId = PersonTestData.Person1Id;

        var repository = new FakePersonRepository();
        repository.SetPersons(PersonTestData.CreatePersons());

        var useCase = new UpdatePersonUseCase(repository);

        var request = new UpdatePersonRequest
        {
            Id = expectedId,
            Name = "Eduardo Updated",
            Age = 31
        };

        var response = await useCase.ExecuteAsync(request, CancellationToken);

        Assert.NotNull(response);
        Assert.Equal(expectedId, repository.UpdatedPerson!.Id);
        Assert.Equal("Eduardo Updated", response.Name);
        Assert.Equal(31, response.Age);

        Assert.Equal(1, repository.GetByIdAsyncCallCount);
        Assert.Equal(1, repository.UpdateAsyncCallCount);
        Assert.NotNull(repository.UpdatedPerson);
        Assert.Equal("Eduardo Updated", repository.UpdatedPerson!.Name);
        Assert.Equal(31, repository.UpdatedPerson.Age);
    }
}