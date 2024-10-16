using Microsoft.AspNetCore.Mvc.RazorPages; // To use PageModel.
using Northwind.EntityModels; // To use Customer.

namespace Northwind.Web.Pages;

/*
This class CustomersModel inherits from PageModel and is used to manage data and behavior 
for a Razor Pages application.

Class Methods:

Constructor CustomersModel(NorthwindContext db): Initializes a new instance of the class, 
    injecting a NorthwindContext object (db) and assigning it to the private field _db.

OnGet(): Handles the HTTP GET request, populating the CustomersByCountry property by grouping 
    customers by country using the _db context.
*/

public class CustomersModel : PageModel
{
    public ILookup<string?, Customer>? CustomersByCountry;

    private NorthwindContext _db;

    /// <summary>
    /// Constructor for CustomersModel.
    /// </summary>
    /// <param name="db">The NorthwindContext to use</param>
    /// <remarks>
    /// This constructor is used to inject the NorthwindContext into the
    /// CustomersModel class. The NorthwindContext is used to interact with the
    /// database.
    /// </remarks>
    public CustomersModel(NorthwindContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Handles the HTTP GET request, populating the CustomersByCountry property by grouping 
    /// customers by country using the _db context.
    /// </summary>
    public void OnGet()
    {
        CustomersByCountry = _db.Customers.ToLookup(c => c.Country);
    }
}