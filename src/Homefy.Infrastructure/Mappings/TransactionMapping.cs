using Homefy.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homefy.Infrastructure.Mappings;

public sealed class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(transaction => transaction.Id);

        builder.Property(transaction => transaction.Description)
            .IsRequired()
            .HasMaxLength(Transaction.MaxDescriptionLength);

        builder.Property(transaction => transaction.Amount)
            .IsRequired()
            .HasColumnType("numeric(18,2)");

        builder.Property(transaction => transaction.Type)
            .IsRequired()
            .HasConversion<string>();

        // Delete Cascade: ao excluir Person, deleta todas as suas Transactions
        builder.HasOne(transaction => transaction.Person)
            .WithMany()
            .HasForeignKey(transaction => transaction.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(transaction => transaction.Category)
            .WithMany()
            .HasForeignKey(transaction => transaction.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); // Categoria não pode ser deletada se houver transações associadas a ela
    }
}