using Homefy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homefy.Infrastructure.Mappings;

public sealed class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Description)
            .IsRequired()
            .HasMaxLength(Category.MaxDescriptionLength);

        builder.Property(category => category.Purpose)
            .IsRequired()
            .HasConversion<string>();
    }
}