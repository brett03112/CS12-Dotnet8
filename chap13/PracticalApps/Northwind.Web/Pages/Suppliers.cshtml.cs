using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Northwind.EntityModels; // To use NorthwindContext

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{
    private NorthwindContext _db;

    [BindProperty]
    public Supplier? Supplier { get; set; }

    /// <summary>
    /// Handles the HTTP POST request sent from the form to add a supplier.
    /// If the Supplier is not null and the ModelState is valid, it adds the supplier to the database
    /// and redirects to the Suppliers page. Otherwise, it returns to the original page.
    /// </summary>
    public IActionResult OnPost()
    {
        if (Supplier is not null && ModelState.IsValid)
        {
            _db.Suppliers.Add(Supplier);
            _db.SaveChanges();

            return RedirectToPage("/Suppliers");
        }
        else
        {
            return Page();  // return to the original page
        }
    }

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

