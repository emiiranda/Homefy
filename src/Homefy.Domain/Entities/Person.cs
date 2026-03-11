using Homefy.Domain.Common;
using Homefy.Domain.Exceptions;

namespace Homefy.Domain.Entities;

public class Person : EntityBase
{
    public const int MaxNameLength = 200;

    public string Name { get; private set; } = string.Empty;
    public int Age { get; private set; }

    

    private Person()
    {
    }

    public Person(string name, int age)
    {
        SetName(name);
        SetAge(age);
    }

    public void Update(string name, int age)
    {
        SetName(name);
        SetAge(age);
    }

    public bool IsMinor()
    {
        return Age < 18;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Person name is required.");

        name = name.Trim();

        if (name.Length > MaxNameLength)
            throw new DomainException($"Person name cannot exceed {MaxNameLength} characters.");

        Name = name;
    }

    private void SetAge(int age)
    {
        if (age < 0)
            throw new DomainException("Person age cannot be negative.");

        Age = age;
    }
}