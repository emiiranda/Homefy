using Homefy.Application.DTOs.Person;
using Homefy.Application.Interfaces.Repositories;

namespace Homefy.Application.UseCases.Persons;

public sealed class GetAllPersonsUseCase(IPersonRepository personRepository)
{
    private readonly IPersonRepository _personRepository =
        personRepository ?? throw new ArgumentNullException(nameof(personRepository));

    public async Task<IReadOnlyList<GetAllPersonsResponse>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var persons = await _personRepository.GetAllAsync(cancellationToken);

        return persons
            .Select(person => new GetAllPersonsResponse
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age
            })
            .ToList();
    }
}