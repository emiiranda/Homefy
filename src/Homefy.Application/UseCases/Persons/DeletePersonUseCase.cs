using Homefy.Application.DTOs.Person;
using Homefy.Application.Interfaces.Repositories;

namespace Homefy.Application.UseCases.Persons;

public sealed class DeletePersonUseCase(IPersonRepository personRepository)
{
    private readonly IPersonRepository _personRepository =
        personRepository ?? throw new ArgumentNullException(nameof(personRepository));

    public async Task<DeletePersonResponse> ExecuteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var person = await _personRepository.GetByIdAsync(id, cancellationToken) 
            ?? throw new KeyNotFoundException($"Person with id '{id}' was not found.");

        await _personRepository.DeleteAsync(person, cancellationToken);

        return new DeletePersonResponse
        {
            Id = id,
        };
    }
}