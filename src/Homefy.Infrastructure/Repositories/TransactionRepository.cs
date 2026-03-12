using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Entities;
using Homefy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Homefy.Infrastructure.Repositories;

public sealed class TransactionRepository(HomefyDbContext context) : ITransactionRepository
{
    private readonly HomefyDbContext _context =
        context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Include(transaction => transaction.Person)
            .Include(transaction => transaction.Category)
            .OrderBy(transaction => transaction.Person.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}