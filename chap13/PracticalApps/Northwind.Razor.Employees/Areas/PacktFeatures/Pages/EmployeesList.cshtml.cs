using Microsoft.AspNetCore.Mvc.RazorPages; // To use PageModel.
using Northwind.EntityModels; // To use Employee, NorthwindContext.

namespace PacktFeatures.Pages;

/// <summary>
/// This class, EmployeesListPageModel, is a page model in an ASP.NET Core Razor Pages application. 
/// It is responsible for handling the data and logic for a page that displays a list of employees.
/// </summary>
/// <methods>
/// Constructor (EmployeesListPageModel): Initializes a new instance of the page model, injecting a 
/// NorthwindContext object for database operations.
/// 
/// OnGet Method: Prepares the page before it is displayed by setting the page title and retrieving 
/// a list of employees from the database, sorted by last name and then first name.
/// </methods>
public class EmployeesListPageModel : PageModel
{
    private NorthwindContext _db;

    /// <summary>
    /// Constructor for EmployeesListPageModel.
    /// </summary>
    /// <param name="db">NorthwindContext for database operations.</param>
    public EmployeesListPageModel(NorthwindContext db)
    {
        _db = db;
    }

    public Employee[] Employees { get; set; } = null!;

    /// <summary>
    /// Page handler to prepare this page before it is displayed.
    /// We initialize ViewData["Title"] and the Employees property.
    /// </summary>
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Employees";

        Employees = _db.Employees.OrderBy(e => e.LastName)
          .ThenBy(e => e.FirstName).ToArray();
    }
}