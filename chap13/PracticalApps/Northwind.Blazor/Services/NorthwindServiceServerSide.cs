using Northwind.Blazor.Services;
using Microsoft.EntityFrameworkCore; // To use ToListAsync<T>.

namespace Northwind.Blazor.Services;
public class NorthwindServiceServerSide : INorthwindService
{
    private readonly NorthwindContext _db;
    
    /// <summary>
    /// Constructor that takes a NorthwindContext as a parameter to initialize 
    /// the _db field, which is used in the other methods of this class to access
    /// the database.
    /// </summary>
    /// <param name="db">The NorthwindContext to use to access the database</param>
    public NorthwindServiceServerSide(NorthwindContext db)
    {
        _db = db;
    }
    
    /// <summary>
    /// Retrieves a list of all customers from the database.
    /// </summary>
    /// <returns>A list of all customers in the database</returns>
    
    public Task<List<Customer>> GetCustomersAsync()
    {
        return _db.Customers.ToListAsync();
    }
    
    /// <summary>
    /// Retrieves a list of customers from the database with the specified country.
    /// </summary>
    /// <param name="country">The country to filter the customers by</param>
    /// <returns>A list of customers with the specified country</returns>
    
    public Task<List<Customer>> GetCustomersAsync(string country)
    {
        return _db.Customers.Where(c => c.Country == country).ToListAsync();
    }
    
    /// <summary>
    /// Retrieves a customer with the specified id from the database.
    /// </summary>
    /// <param name="id">The id of the customer to retrieve</param>
    /// <returns>The customer with the specified id, or null if no customer is found.</returns>
    public Task<Customer?> GetCustomerAsync(string id)
    {
        return _db.Customers.FirstOrDefaultAsync
        (c => c.CustomerId == id);
    }
    
    /// <summary>
    /// Creates a new customer in the database.
    /// </summary>
    /// <param name="c">The customer to create</param>
    /// <returns>The customer that was created</returns>
    public Task<Customer> CreateCustomerAsync(Customer c)
    {
        _db.Customers.Add(c);
        _db.SaveChangesAsync();
        return Task.FromResult(c);
    }
    
    /// <summary>
    /// Updates a customer in the database.
    /// </summary>
    /// <param name="c">The customer to update</param>
    /// <returns>The customer that was updated</returns>
    public Task<Customer> UpdateCustomerAsync(Customer c)
    {
        _db.Entry(c).State = EntityState.Modified;
        _db.SaveChangesAsync();
        return Task.FromResult(c);
    }
    
    /// <summary>
    /// Deletes a customer with the specified id from the database.
    /// </summary>
    /// <param name="id">The id of the customer to delete</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task DeleteCustomerAsync(string id)
    {
        Customer? customer = _db.Customers.FirstOrDefaultAsync
        (c => c.CustomerId == id).Result;
        if (customer == null)
        {
            return Task.CompletedTask;
        }
        else
        {
            _db.Customers.Remove(customer);
            return _db.SaveChangesAsync();
        }
    }
}