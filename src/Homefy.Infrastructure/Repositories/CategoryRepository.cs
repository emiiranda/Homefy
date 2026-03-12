using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Entities;
using Homefy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Homefy.Infrastructure.Repositories;

public sealed class CategoryRepository(HomefyDbContext context) : ICategoryRepository
{
    private readonly HomefyDbContext _context =
        context ?? throw new ArgumentNullException(nameof(context));

    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(category => category.Description)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id, cancellationToken);
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}