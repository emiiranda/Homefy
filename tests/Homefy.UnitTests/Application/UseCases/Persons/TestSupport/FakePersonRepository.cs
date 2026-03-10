using System.Reflection;
using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Common;
using Homefy.Domain.Entities;

namespace Homefy.UnitTests.Application.UseCases.Persons.TestSupport;

internal sealed class FakePersonRepository : IPersonRepository
{
    private readonly List<Person> _persons = [];

    public int AddAsyncCallCount { get; private set; }
    public int GetAllAsyncCallCount { get; private set; }
    public int GetByIdAsyncCallCount { get; private set; }
    public int UpdateAsyncCallCount { get; private set; }
    public int DeleteAsyncCallCount { get; private set; }

    public Guid? LastRequestedId { get; private set; }
    public Person? AddedPerson { get; private set; }
    public Person? UpdatedPerson { get; private set; }
    public Person? DeletedPerson { get; private set; }

    public void SetPersons(IEnumerable<Person> persons)
    {
        _persons.Clear();
        _persons.AddRange(persons);
    }

    public Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        GetByIdAsyncCallCount++;
        LastRequestedId = id;

        var person = _persons.FirstOrDefault(person => person.Id == id);
        return Task.FromResult(person);
    }

    public Task<List<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        GetAllAsyncCallCount++;
        return Task.FromResult(_persons.ToList());
    }

    public Task AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        AddAsyncCallCount++;
        AddedPerson = person;

        if (person.Id == Guid.Empty)
        {
            SetEntityId(person, Guid.NewGuid());
        }

        _persons.Add(person);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        UpdateAsyncCallCount++;
        UpdatedPerson = person;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Person person, CancellationToken cancellationToken = default)
    {
        DeleteAsyncCallCount++;
        DeletedPerson = person;

        _persons.Remove(person);

        return Task.CompletedTask;
    }

    private static void SetEntityId(EntityBase entity, Guid id)
    {
        var property = typeof(EntityBase).GetProperty(
            nameof(EntityBase.Id),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        property!.SetValue(entity, id);
    }
}