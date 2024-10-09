using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // To use IServiceCollection

namespace Northwind.EntityModels;

public static class NorthwindContextExtensions
{

    /// <summary>
    /// Adds a <see cref="NorthwindContext"/> to the provided
    /// <see cref="IServiceCollection"/>, using the specified relative
    /// path and database name.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the
    /// <see cref="NorthwindContext"/> to.</param>
    /// <param name="relativePath">The relative path to the database file, relative
    /// to the root directory of the project. The default is "..", the parent
    /// directory of the project's root directory.</param>
    /// <param name="databaseName">The name of the database file to use. The
    /// default is "Northwind.db".
    /// </param>
    /// <returns>The <see cref="IServiceCollection"/> with the
    /// <see cref="NorthwindContext"/> added to it.</returns>
    public static IServiceCollection AddNorthwindContext(
        this IServiceCollection services, // The type to extend.
        string relativePath = "..",
        string databaseName = "Northwind.db")
    {
        string path = Path.Combine(relativePath, databaseName);
        path = Path.GetFullPath(path);
        NorthwindContextLogger.WriteLine($"Database path: {path}");
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
            message: $"{path} not found.", fileName: path);
        }
        services.AddDbContext<NorthwindContext>(options =>
        {
            // Data Source is the modern equivalent of Filename.
            options.UseSqlite($"Data Source={path}");
            options.LogTo(NorthwindContextLogger.WriteLine,
                new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
        },
        // Register with a transient lifetime to avoid concurrency
        // issues in Blazor server-side projects.
        contextLifetime: ServiceLifetime.Transient,
        optionsLifetime: ServiceLifetime.Transient);
        return services;
    }
}