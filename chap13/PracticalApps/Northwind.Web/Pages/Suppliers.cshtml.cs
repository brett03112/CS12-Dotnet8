using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.EntityModels; // To use NorthwindContext

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{
    private NorthwindContext _db;

    /// <summary>
    /// Constructor for SuppliersModel
    /// </summary>
    /// <param name="db">The NorthwindContext to use</param>
    /// <remarks>
    /// This constructor is used to inject the NorthwindContext into the
    /// SuppliersModel class. The NorthwindContext is used to interact with
    /// the database.
    /// </remarks>
    public SuppliersModel(NorthwindContext db)
    {
        _db = db;
    }
    public IEnumerable<Supplier>? Suppliers { get; set; }

    /// <summary>
    /// OnGet is called when the GET request is made for this PageModel.
    /// It sets ViewData["Title"] and populates the Suppliers property.
    /// </summary>
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";

        Suppliers = _db.Suppliers
            .OrderBy(c => c.Country)
            .ThenBy(c => c.CompanyName);
    }
}

