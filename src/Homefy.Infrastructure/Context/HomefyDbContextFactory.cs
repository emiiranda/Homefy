using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Homefy.Infrastructure.Context;

public sealed class HomefyDbContextFactory : IDesignTimeDbContextFactory<HomefyDbContext>
{
    public HomefyDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "Homefy.Api");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        var optionsBuilder = new DbContextOptionsBuilder<HomefyDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new HomefyDbContext(optionsBuilder.Options);
    }
}