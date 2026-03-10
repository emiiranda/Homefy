using Homefy.Domain.Entities;

namespace Homefy.Application.Interfaces.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Person>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Person person, CancellationToken cancellationToken = default);
    Task UpdateAsync(Person person, CancellationToken cancellationToken = default);
    Task DeleteAsync(Person person, CancellationToken cancellationToken = default);
}