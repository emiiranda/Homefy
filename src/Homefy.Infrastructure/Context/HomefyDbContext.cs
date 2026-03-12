using Homefy.Domain.Entities;
using Homefy.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Homefy.Infrastructure.Context;

public sealed class HomefyDbContext(DbContextOptions<HomefyDbContext> options) : DbContext(options)
{
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonMapping());
        modelBuilder.ApplyConfiguration(new CategoryMapping());

        base.OnModelCreating(modelBuilder);
    }
}