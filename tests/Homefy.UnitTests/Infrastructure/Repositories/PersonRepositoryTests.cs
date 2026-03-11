using Homefy.Domain.Entities;
using Homefy.Infrastructure.Context;
using Homefy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Homefy.UnitTests.Infrastructure.Repositories;

public class PersonRepositoryTests
{
    private static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

    private static HomefyDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<HomefyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new HomefyDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldPersistPerson_WhenPersonIsValid()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);
        var person = new Person("Eduardo Miranda", 30);

        // Act
        await repository.AddAsync(person, CancellationToken);

        // Assert
        var persisted = await context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == person.Id, 
            CancellationToken);

        Assert.NotNull(persisted);
        Assert.Equal(person.Id, persisted.Id);
        Assert.Equal(person.Name, persisted.Name);
        Assert.Equal(person.Age, persisted.Age);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);
        var person = new Person("Eduardo Miranda", 30);
        await repository.AddAsync(person, CancellationToken);

        // Act
        var result = await repository.GetByIdAsync(person.Id, CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(person.Id, result.Id);
        Assert.Equal(person.Name, result.Name);
        Assert.Equal(person.Age, result.Age);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenPersonDoesNotExist()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);

        // Act
        var result = await repository.GetByIdAsync(Guid.NewGuid(), CancellationToken);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoPersonsExist()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);

        // Act
        var result = await repository.GetAllAsync(CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPersons_WhenPersonsExist()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);
        await repository.AddAsync(new Person("Eduardo Miranda", 30), CancellationToken);
        await repository.AddAsync(new Person("Pedro Felipe", 25), CancellationToken);

        // Act
        var result = await repository.GetAllAsync(CancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnPersonsOrderedByName()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);
        await repository.AddAsync(new Person("Pedro Felipe", 17), CancellationToken);
        await repository.AddAsync(new Person("Paulo Barbosa", 34), CancellationToken);
        await repository.AddAsync(new Person("Mariana Elisa", 32), CancellationToken);

        // Act
        var result = await repository.GetAllAsync(CancellationToken);

        // Assert
        Assert.Equal("Mariana Elisa", result[0].Name);
        Assert.Equal("Paulo Barbosa", result[1].Name);
        Assert.Equal("Pedro Felipe", result[2].Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyPerson_WhenPersonExists()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);
        var person = new Person("Eduardo", 20);
        await repository.AddAsync(person, CancellationToken);

        // Act
        person.Update("Eduardo Miranda", 31);
        await repository.UpdateAsync(person, CancellationToken);

        // Assert
        var updated = await context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == person.Id, CancellationToken);

        Assert.NotNull(updated);
        Assert.Equal("Eduardo Miranda", updated.Name);
        Assert.Equal(31, updated.Age);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemovePerson_WhenPersonExists()
    {
        // Arrange
        await using var context = CreateContext();
        var repository = new PersonRepository(context);
        var person = new Person("Eduardo Miranda", 30);
        await repository.AddAsync(person, CancellationToken);

        // Act
        await repository.DeleteAsync(person, CancellationToken);

        // Assert
        var deleted = await context.Persons
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == person.Id, CancellationToken);

        Assert.Null(deleted);
    }
}