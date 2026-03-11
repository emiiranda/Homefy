using Homefy.Application.DependencyInjection;
using Homefy.Infrastructure.DependencyInjection;

namespace Homefy.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}