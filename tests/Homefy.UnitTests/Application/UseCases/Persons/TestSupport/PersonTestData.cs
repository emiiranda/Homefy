using System.Reflection;
using Homefy.Application.DTOs.Person;
using Homefy.Domain.Common;
using Homefy.Domain.Entities;

namespace Homefy.UnitTests.Application.UseCases.Persons.TestSupport;

internal static class PersonTestData
{
    public static readonly Guid Person1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid Person2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");

    public static CreatePersonRequest CreateValidRequest()
    {
        return new CreatePersonRequest
        {
            Name = "Eduardo Miranda",
            Age = 30
        };
    }

    public static CreatePersonRequest CreateRequestWithInvalidName()
    {
        return new CreatePersonRequest
        {
            Name = string.Empty,
            Age = 25
        };
    }

    public static CreatePersonRequest CreateRequestWithInvalidAge()
    {
        return new CreatePersonRequest
        {
            Name = "Eduardo Miranda",
            Age = -1
        };
    }

    public static List<Person> CreatePersons()
    {
        var person1 = new Person("Eduardo Miranda", 30);
        SetEntityId(person1, Person1Id);

        var person2 = new Person("Pedro Felipe", 17);
        SetEntityId(person2, Person2Id);

        return [person1, person2];
    }

    private static void SetEntityId(EntityBase entity, Guid id)
    {
        var property = typeof(EntityBase).GetProperty(
            nameof(EntityBase.Id),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        property!.SetValue(entity, id);
    }
}