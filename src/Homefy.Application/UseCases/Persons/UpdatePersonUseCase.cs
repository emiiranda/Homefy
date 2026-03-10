using Homefy.Application.DTOs.Person;
using Homefy.Application.Interfaces.Repositories;

namespace Homefy.Application.UseCases.Persons;

public sealed class UpdatePersonUseCase(IPersonRepository personRepository)
{
    private readonly IPersonRepository _personRepository =
        personRepository ?? throw new ArgumentNullException(nameof(personRepository));

    public async Task<UpdatePersonResponse> ExecuteAsync(
        UpdatePersonRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw new KeyNotFoundException($"Person with id '{request.Id}' was not found.");
        person.Update(request.Name, request.Age);

        await _personRepository.UpdateAsync(person, cancellationToken);

        return new UpdatePersonResponse
        {
            Name = person.Name,
            Age = person.Age
        };
    }
}