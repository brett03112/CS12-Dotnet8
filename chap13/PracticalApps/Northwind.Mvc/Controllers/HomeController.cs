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
    /// The controller that handles HTTP requests for the home page.
    /// </summary>
    /// <param name="logger">The logger to write diagnostic messages to.</param>
    /// <param name="db">The database context to store and retrieve data from.</param>
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

    // This action method will handle GET and other requests except POST
    public IActionResult ModelBinding()
    {
        return View(); // The page with a form to submit
    }

        
    
    /// <summary>
    /// The function ModelBinding in C# creates a view model based on a Thing object and ModelState
    /// errors for rendering in a view.
    /// </summary>
    /// <param name="Thing">The `Thing` parameter in the `ModelBinding` method is an object of type
    /// `Thing` that is being passed as a parameter to the action method. This object likely contains
    /// data that was submitted in a form or as part of a request, and the method is using model binding
    /// to bind this</param>
    /// <returns>
    /// The code snippet is returning a View with a `HomeModelBindingViewModel` model. The
    /// `HomeModelBindingViewModel` model is being initialized with the `Thing` object, a boolean value
    /// indicating if there are validation errors (`!ModelState.IsValid`), and a list of validation
    /// error messages extracted from the `ModelState`.
    /// </returns>
    [HttpPost] // This action method will handle POST requests
    public IActionResult ModelBinding(Thing thing)
    {
        HomeModelBindingViewModel model = new(
            Thing: thing, HasErrors: !ModelState.IsValid,
            ValidationErrors: ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
        );

        return View(model); // Show the model bound thingS
    }
}
