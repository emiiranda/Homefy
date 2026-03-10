using Homefy.Application.DTOs.Person;
using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Entities;

namespace Homefy.Application.UseCases.Persons;

public sealed class CreatePersonUseCase(IPersonRepository personRepository)
{
    private readonly IPersonRepository _personRepository =
        personRepository ?? throw new ArgumentNullException(nameof(personRepository));

    public async Task<CreatePersonResponse> ExecuteAsync(
        CreatePersonRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var person = new Person(request.Name, request.Age);

        await _personRepository.AddAsync(person, cancellationToken);

        return new CreatePersonResponse
        {
            Id = person.Id,
            Name = person.Name,
            Age = person.Age
        };
    }
}