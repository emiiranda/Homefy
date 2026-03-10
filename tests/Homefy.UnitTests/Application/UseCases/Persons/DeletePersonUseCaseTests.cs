using Homefy.Application.UseCases.Persons;
using Homefy.UnitTests.Application.UseCases.Persons.TestSupport;


namespace Homefy.UnitTests.Application.UseCases.Persons;

public sealed class DeletePersonUseCaseTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        var act = () => new DeletePersonUseCase(null!);

        var exception = Assert.Throws<ArgumentNullException>(act);
        Assert.Equal("personRepository", exception.ParamName);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldThrowKeyNotFoundException_WhenPersonDoesNotExist()
    {
        var repository = new FakePersonRepository();
        var useCase = new DeletePersonUseCase(repository);
        var id = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var act = () => useCase.ExecuteAsync(id, TestContext.Current.CancellationToken);

        await Assert.ThrowsAsync<KeyNotFoundException>(act);
        Assert.Equal(1, repository.GetByIdAsyncCallCount);
        Assert.Equal(0, repository.DeleteAsyncCallCount);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldDeletePersonAndReturnResponse()
    {
        var repository = new FakePersonRepository();
        repository.SetPersons(PersonTestData.CreatePersons());

        var useCase = new DeletePersonUseCase(repository);

        var response = await useCase.ExecuteAsync(
            PersonTestData.Person1Id,
            TestContext.Current.CancellationToken);

        Assert.NotNull(response);
        Assert.Equal(PersonTestData.Person1Id, response.Id);

        Assert.Equal(1, repository.GetByIdAsyncCallCount);
        Assert.Equal(1, repository.DeleteAsyncCallCount);
        Assert.NotNull(repository.DeletedPerson);
        Assert.Equal(PersonTestData.Person1Id, repository.DeletedPerson!.Id);
    }
}