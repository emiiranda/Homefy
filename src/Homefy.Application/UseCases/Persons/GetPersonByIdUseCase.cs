using Homefy.Application.DTOs.Person;
using Homefy.Application.Interfaces.Repositories;

namespace Homefy.Application.UseCases.Persons;

public sealed class GetPersonByIdUseCase(IPersonRepository personRepository)
{
    private readonly IPersonRepository _personRepository =
        personRepository ?? throw new ArgumentNullException(nameof(personRepository));

    public async Task<GetPersonByIdResponse> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var person = await _personRepository.GetByIdAsync(id, cancellationToken);

        return person is null
            ? throw new KeyNotFoundException($"Person with id '{id}' was not found.")
            : new GetPersonByIdResponse
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            };
    }
}