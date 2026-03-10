using Homefy.Application.UseCases.Persons;
using Homefy.UnitTests.Application.UseCases.Persons.TestSupport;

namespace Homefy.UnitTests.Application.UseCases.Persons;

public sealed class GetAllPersonsUseCaseTests
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenRepositoryIsNull()
    {
        static GetAllPersonsUseCase act() => new(null!);

        var exception = Assert.Throws<ArgumentNullException>((Func<GetAllPersonsUseCase>)act);
        Assert.Equal("personRepository", exception.ParamName);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnAllPersons()
    {
        var repository = new FakePersonRepository();
        repository.SetPersons(PersonTestData.CreatePersons());

        var useCase = new GetAllPersonsUseCase(repository);

        var response = await useCase.ExecuteAsync(TestContext.Current.CancellationToken);

        Assert.NotNull(response);
        Assert.Equal(2, response.Count);

        Assert.Collection(response,
            first =>
            {
                Assert.Equal(PersonTestData.Person1Id, first.Id);
                Assert.Equal("Eduardo Miranda", first.Name);
                Assert.Equal(30, first.Age);
            },
            second =>
            {
                Assert.Equal(PersonTestData.Person2Id, second.Id);
                Assert.Equal("Pedro Felipe", second.Name);
                Assert.Equal(17, second.Age);
            });

        Assert.Equal(1, repository.GetAllAsyncCallCount);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenThereAreNoPersons()
    {
        var repository = new FakePersonRepository();
        var useCase = new GetAllPersonsUseCase(repository);

        var response = await useCase.ExecuteAsync(TestContext.Current.CancellationToken);

        Assert.NotNull(response);
        Assert.Empty(response);
        Assert.Equal(1, repository.GetAllAsyncCallCount);
    }
}