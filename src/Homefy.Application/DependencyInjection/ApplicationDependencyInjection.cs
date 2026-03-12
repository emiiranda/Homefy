using Homefy.Application.UseCases.Categories;
using Homefy.Application.UseCases.Persons;
using Homefy.Application.UseCases.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace Homefy.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Persons
        services.AddScoped<CreatePersonUseCase>();
        services.AddScoped<GetAllPersonsUseCase>();
        services.AddScoped<GetPersonByIdUseCase>();
        services.AddScoped<UpdatePersonUseCase>();
        services.AddScoped<DeletePersonUseCase>();
        services.AddScoped<GetPersonTotalsUseCase>();

        // Categories
        services.AddScoped<CreateCategoryUseCase>();
        services.AddScoped<GetAllCategoriesUseCase>();
        services.AddScoped<GetCategoryTotalsUseCase>();

        // Transactions
        services.AddScoped<CreateTransactionUseCase>();
        services.AddScoped<GetAllTransactionsUseCase>();

        return services;
    }
}