using Homefy.Application.UseCases.Persons;
using Microsoft.Extensions.DependencyInjection;

namespace Homefy.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreatePersonUseCase>();
        services.AddScoped<GetAllPersonsUseCase>();
        services.AddScoped<GetPersonByIdUseCase>();
        services.AddScoped<UpdatePersonUseCase>();
        services.AddScoped<DeletePersonUseCase>();

        return services;
    }
}