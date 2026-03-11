using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Entities;
using Homefy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Homefy.Infrastructure.Repositories;

public sealed class PersonRepository(HomefyDbContext context) : IPersonRepository
{
    private readonly HomefyDbContext _context =
        context ?? throw new ArgumentNullException(nameof(context));

    public async Task<Person?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Persons
            .FirstOrDefaultAsync(person => person.Id == id, cancellationToken);
    }

    public async Task<List<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Persons
            .AsNoTracking()
            .OrderBy(person => person.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken = default)
    {
        await _context.Persons.AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken = default)
    {
        _context.Persons.Update(person);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Person person, CancellationToken cancellationToken = default)
    {
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync(cancellationToken);
    }
}