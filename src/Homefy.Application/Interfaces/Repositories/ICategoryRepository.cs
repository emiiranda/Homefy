using Homefy.Domain.Entities;

namespace Homefy.Application.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
}