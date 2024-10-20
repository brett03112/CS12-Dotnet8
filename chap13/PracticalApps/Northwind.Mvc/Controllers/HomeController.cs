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

    /// <summary>
    /// The default constructor for HomeController that takes a logger and a database context.
    /// </summary>
    /// <param name="logger">The logger to use for logging.</param>
    /// <param name="db">The database context to use for accessing and manipulating data.</param>
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

    /// <summary>
    /// The ProductDetail function in C# retrieves product details based on the provided ID and returns
    /// a corresponding view.
    /// </summary>
    /// <param name="id">The `ProductDetail` action in the code snippet is a method in a controller that
    /// retrieves details of a product based on the provided `id`. The `id` parameter is of type `int?`,
    /// which means it can be nullable.</param>
    /// <returns>
    /// The `ProductDetail` action in the code snippet is returning an `IActionResult`. Depending on the
    /// conditions, it can return different types of results:
    /// </returns>
    public IActionResult ProductDetail(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest("You must pass a product ID in the route, EX: /Home/ProductDetail/21");
        }

        Product? model = _db.Products.Include(p => p.Category)
            .SingleOrDefault(p => p.ProductId == id);

        if (model == null)
        {
            return NotFound($"ProductID {id} not found");
        }

        return View(model);
    }
        /// <summary>
        /// The privacy action displays the privacy page.
        /// </summary>
        /// <returns>A view that displays the privacy page.</returns>
    public IActionResult Privacy()
    {
        return View();
    }

        /// <summary>
        /// The error action displays the error page.
        /// </summary>
        /// <returns>
        /// A view that displays the error page.
        /// </returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
