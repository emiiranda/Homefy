using Homefy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homefy.Infrastructure.Mappings;

public sealed class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");

        builder.HasKey(person => person.Id);

        builder.Property(person => person.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(person => person.Age)
            .IsRequired();
    }
}