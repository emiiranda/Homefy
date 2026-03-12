using Homefy.Application.DTOs.Category;
using Homefy.Application.Interfaces.Repositories;
using Homefy.Domain.Enums;

namespace Homefy.Application.UseCases.Categories;

public sealed class GetCategoryTotalsUseCase(
    ICategoryRepository categoryRepository,
    ITransactionRepository transactionRepository)
{
    private readonly ICategoryRepository _categoryRepository =
        categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    private readonly ITransactionRepository _transactionRepository =
        transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));

    public async Task<GetCategoryTotalsResponse> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        var transactions = await _transactionRepository.GetAllAsync(cancellationToken);

        var categoryTotals = categories
            .Select(category =>
            {
                var categoryTransactions = transactions
                    .Where(t => t.CategoryId == category.Id)
                    .ToList();

                var totalIncome = categoryTransactions
                    .Where(t => t.Type == TransactionType.Income)
                    .Sum(t => t.Amount);

                var totalExpense = categoryTransactions
                    .Where(t => t.Type == TransactionType.Expense)
                    .Sum(t => t.Amount);

                return new GetCategoryTotalsItem
                {
                    Id = category.Id,
                    Description = category.Description,
                    Purpose = category.Purpose,
                    TotalIncome = totalIncome,
                    TotalExpense = totalExpense,
                    Balance = totalIncome - totalExpense
                };
            })
            .ToList();

        var grandTotalIncome = categoryTotals.Sum(c => c.TotalIncome);
        var grandTotalExpense = categoryTotals.Sum(c => c.TotalExpense);

        return new GetCategoryTotalsResponse
        {
            Categories = categoryTotals,
            TotalIncome = grandTotalIncome,
            TotalExpense = grandTotalExpense,
            Balance = grandTotalIncome - grandTotalExpense
        };
    }
}