using System.Diagnostics; // To use Activity
using Microsoft.AspNetCore.Mvc; // To use Controller
using Northwind.Mvc.Models; // To use ErrorViewModel
using Northwind.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly NorthwindContext _db;

    public HomeController(ILogger<HomeController> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    /// <summary>
    /// The index action displays the home page with a welcome message that displays the number of visitors.
    /// </summary>
    /// <returns>A view that displays the home page with a welcome message.</returns>
    public IActionResult Index()
    {
        /*
        *****Index Action Method*****

        This C# method, Index, is an action method in a controller class that handles HTTP requests. 
        It returns a view that displays the home page with a welcome message.

        Key Points:

        Creates a new instance of HomeIndexViewModel with:

            A random VisitorCount between 1 and 1001.
        
            A list of categories from the database (_db.Categories.ToList()).
        
            A list of products from the database (_db.Products.ToList()).
        
            Returns a view, passing the HomeIndexViewModel instance as the model.
        
        ***Note: The return View(); statement should be return View(model); to pass the model to the view.
        */
        HomeIndexViewModel model = new
        (
            VisitorCount: Random.Shared.Next(1, 1001),
            Categories: _db.Categories.ToList(),
            Products: _db.Products.ToList()
        );
        
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
