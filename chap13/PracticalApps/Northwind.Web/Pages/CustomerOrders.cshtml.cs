using Microsoft.AspNetCore.Mvc.RazorPages; // To use PageModel.
using Microsoft.EntityFrameworkCore; // To use Include method.
using Northwind.EntityModels; // To use Customer.

namespace Northwind.Web.Pages;

/*
Class Definition:

The CustomerOrdersModel class is a PageModel that handles customer order data, 
interacting with the database using the NorthwindContext.

Class Methods:

Constructor (CustomerOrdersModel(NorthwindContext db)): Initializes the class by injecting the 
    NorthwindContext instance, which is used to interact with the database.

OnGet() method: Handles HTTP GET requests, retrieving a customer's data (including orders) 
    from the database based on the provided id parameter and populating the Customer property.
*/
public class CustomerOrdersModel : PageModel
{
    public Customer? Customer;

    private NorthwindContext _db;

    /// <summary>
    /// Constructor for CustomerOrdersModel.
    /// </summary>
    /// <param name="db">The NorthwindContext to use</param>
    /// <remarks>
    /// This constructor is used to inject the NorthwindContext into the
    /// CustomerOrdersModel class. The NorthwindContext is used to interact with
    /// the database.
    /// </remarks>
    public CustomerOrdersModel(NorthwindContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Handles the HTTP GET request, populating the Customer property by getting the customer
    /// with the specified id and including the orders.
    /// </summary>
    public void OnGet()
    {
        string? id = HttpContext.Request.Query["id"];

        Customer = _db.Customers.Include(c => c.Orders)
            .SingleOrDefault(c => c.CustomerId == id);
    }
}