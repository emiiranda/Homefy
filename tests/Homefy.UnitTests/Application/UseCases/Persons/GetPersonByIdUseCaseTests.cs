using Homefy.Application.UseCases.Persons;
using Homefy.UnitTests.Application.UseCases.Persons.TestSupport;
using Homefy.Application.DTOs.Person;

namespace Homefy.UnitTests.Application.UseCases.Persons;

public sealed class GetPersonByIdUseCaseTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        static GetPersonByIdUseCase act() => new(null!);

        var exception = Assert.Throws<ArgumentNullException>((Func<GetPersonByIdUseCase>)act);
        Assert.Equal("personRepository", exception.ParamName);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPerson_WhenPersonExists()
    {
        var repository = new FakePersonRepository();
        repository.SetPersons(PersonTestData.CreatePersons());

        var useCase = new GetPersonByIdUseCase(repository);

        var response = await useCase.ExecuteAsync(
            PersonTestData.Person1Id,
            TestContext.Current.CancellationToken);

        Assert.NotNull(response);
        Assert.Equal(PersonTestData.Person1Id, response.Id);
        Assert.Equal("Eduardo Miranda", response.Name);
        Assert.Equal(30, response.Age);

        Assert.Equal(1, repository.GetByIdAsyncCallCount);
        Assert.Equal(PersonTestData.Person1Id, repository.LastRequestedId);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
    {
        var repository = new FakePersonRepository();
        repository.SetPersons(PersonTestData.CreatePersons());

        var useCase = new GetPersonByIdUseCase(repository);
        var nonExistentId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        Task<GetPersonByIdResponse> act() => useCase.ExecuteAsync(
            nonExistentId,
            TestContext.Current.CancellationToken);

        await Assert.ThrowsAsync<KeyNotFoundException>((Func<Task<GetPersonByIdResponse>>)act);

        Assert.Equal(1, repository.GetByIdAsyncCallCount);
        Assert.Equal(nonExistentId, repository.LastRequestedId);
    }
}