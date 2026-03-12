using Homefy.Domain.Entities;

namespace Homefy.Application.Interfaces.Repositories;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
}