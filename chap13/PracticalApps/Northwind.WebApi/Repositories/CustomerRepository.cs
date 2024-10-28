using Microsoft.EntityFrameworkCore.ChangeTracking; // To use EntityEntry
using Northwind.EntityModels; // To use Customer class
using Microsoft.Extensions.Caching.Memory; // To use IMemoryCache
using Microsoft.EntityFrameworkCore; // To use ToArrayAsync method


namespace Northwind.WebApi.Repositories;
/*
Here is a succinct explanation of the CustomerRepository class definition:

Class Overview

The CustomerRepository class is a data access layer that manages customer data in a 
database and an in-memory cache. It implements the ICustomerRepository interface.

Class Methods

    Constructor: Initializes the repository with a NorthwindContext database context and an 
    IMemoryCache cache instance.
    
    CreateAsync: Adds a new customer to the database and cache, returning the added customer 
    or null if the operation fails.
    
    RetrieveAllAsync: Retrieves all customers from the database, returning an array of customers.
    
    RetrieveAsync: Retrieves a customer by ID from the cache or database, returning the customer or 
    null if not found.
    
    UpdateAsync: Updates a customer in the database and cache, returning the updated customer or 
    null if not found.
    
    DeleteAsync: Deletes a customer from the database and cache, returning true if successful, 
    false if not found, or null if the operation fails.

Note that the class uses Entity Framework Core (EF Core) for database operations and an in-memory 
cache to improve performance.
*/
public class CustomerRepository : ICustomerRepository
{
    private readonly IMemoryCache _memoryCache;

    private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
    {
        SlidingExpiration = TimeSpan.FromMinutes(30)
    };

    // Use an instance data context field because it should not be cached 
    // due to the data context having internal caching

    private readonly NorthwindContext _db;

    /// <summary>
    /// Constructor for CustomerRepository. 
    /// </summary>
    /// <param name="db">The NorthwindContext to use for database operations.</param>
    /// <param name="memoryCache">The IMemoryCache to use for caching</param>
    public CustomerRepository(NorthwindContext db, IMemoryCache memoryCache)
    {
        _db = db;
        _memoryCache = memoryCache;
    }

    /// <summary>
    /// Asynchronously adds a customer to the database and if successful,
    /// also adds it to the in-memory cache.
    /// </summary>
    /// <param name="c">The customer to add</param>
    /// <returns>The customer added, or null if not added</returns>
    public async Task<Customer?> CreateAsync(Customer c)
    {
        c.CustomerId = c.CustomerId.ToUpper(); // Normalize to uppercase
        // Add database using EF Core
        EntityEntry<Customer> added = await _db.Customers.AddAsync(c);

        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            // If saved to database then store in cache
            // Add cache using IMemoryCache
            _memoryCache.Set(c.CustomerId, c, _cacheEntryOptions);

            return c;
        }

        return null;
    }

    /// <summary>
    /// Asynchronously retrieves all customers from the database.
    /// </summary>
    /// <returns>
    /// An array of customers from EF Core's _db.Customers.ToArrayAsync() method.
    /// </returns>
    public Task<Customer[]> RetrieveAllAsync()
    {
        return _db.Customers.ToArrayAsync();
    }

    /// <summary>
    /// Asynchronously retrieves a customer from the database if it exists, or
    /// from the in-memory cache if it has been retrieved before.
    /// </summary>
    /// <param name="id">The customer ID to retrieve</param>
    /// <returns>The customer with the specified ID, or null if not found</returns>
    public Task<Customer?> RetrieveAsync(string id)
    {
        id = id.ToUpper();

        // Try to get from the cache first
        if (_memoryCache.TryGetValue(id, out Customer? fromCache))
        {
            return Task.FromResult(fromCache);
        }

        // If not in cache, get from database
        Customer? fromDb = _db.Customers.FirstOrDefault(c => c.CustomerId == id);

        //If not in database, return null
        if (fromDb is null) return Task.FromResult(fromDb);

        // If in database, add to cache and return customer
        _memoryCache.Set(fromDb.CustomerId, fromDb, _cacheEntryOptions);
        return Task.FromResult(fromDb)!;
    }

    /// <summary>
    /// Asynchronously updates the customer with the specified ID in the database and the in-memory cache.
    /// </summary>
    /// <param name="c">The customer to update</param>
    /// <returns>The updated customer if the update succeeded, or null if not found</returns>
    public async Task<Customer?> UpdateAsync(Customer c)
    {
        c.CustomerId = c.CustomerId.ToUpper(); // Normalize to uppercase
        _db.Customers.Update(c);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            _memoryCache.Set(c.CustomerId, c, _cacheEntryOptions);
            return c;
        }

        return null;
    }


    /// <summary>
    /// Asynchronously deletes a customer from the database and the in-memory cache.
    /// </summary>
    /// <param name="id">The customer ID to delete</param>
    /// <returns>true if customer was deleted, false if not found, or null if the update failed</returns>
    public async Task<bool?> DeleteAsync(string id)
    {
        id = id.ToUpper();

        Customer? c = await _db.Customers.FindAsync(id);

        if (c is null) return null;

        _db.Customers.Remove(c);
        int affected = await _db.SaveChangesAsync();
        if (affected == 1)
        {
            _memoryCache.Remove(c.CustomerId);
            return true;
        }

        return null;

    }
}

/*
Registering dependency services

You can register dependency services with different lifetimes, as shown in the following list:

    • Transient: These services are created each time they’re requested. Transient services should
    be lightweight and stateless.

    • Scoped: These services are created once per client request and are disposed of then the response 
        returns to the client.
    
    • Singleton: These services are usually created the first time they are requested and then shared,
        although you can provide an instance at the time of registration too.

Introduced in .NET 8 is the ability to set a key for a dependency service. This allows multiple services
to be registered with different keys and then retrieved later using that key.
**************************************************************************************************
builder.Services.AddKeyedSingleton<IMemoryCache, BigCache>("big");
builder.Services.AddKeyedSingleton<IMemoryCache, SmallCache>("small");

class BigCacheConsumer([FromKeyedServices("big")] IMemoryCache cache)
{
    public object? GetData() => cache.Get("data");
}

class SmallCacheConsumer(IKeyedServiceProvider keyedServiceProvider)
{
    public object? GetData() => keyedServiceProvider
        .GetRequiredKeyedService<IMemoryCache>("small");
}
*/