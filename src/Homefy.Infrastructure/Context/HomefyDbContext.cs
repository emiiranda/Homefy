using Homefy.Domain.Entities;
using Homefy.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Homefy.Infrastructure.Context;

public sealed class HomefyDbContext(DbContextOptions<HomefyDbContext> options) : DbContext(options)
{
    public DbSet<Person> Persons => Set<Person>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonMapping());

        base.OnModelCreating(modelBuilder);
    }
}