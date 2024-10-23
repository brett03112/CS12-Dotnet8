using System.Diagnostics; // To use Activity
using Microsoft.AspNetCore.Mvc; // To use Controller
using Northwind.Mvc.Models; // To use ErrorViewModel
using Northwind.EntityModels;
using Microsoft.EntityFrameworkCore; // To use Include and ToListAsync methods

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
    /// The index action displays the home page with a random count of
    /// visitors, a list of categories, and a list of products.
    /// </summary>
    /// <returns>
    /// A view that displays the home page with a random count of visitors,
    /// a list of categories, and a list of products.
    /// </returns>
    [ResponseCache(Duration = 10/* seconds */, Location = ResponseCacheLocation.Any, NoStore = true)]
    public async Task<IActionResult> Index()
    {
        _logger.LogError("This is a serious error (not really!)");
        _logger.LogWarning("This is your first warning!");
        _logger.LogWarning("Second warning!");
        _logger.LogInformation("I am in the Index method of the HomeController.");
        
                HomeIndexViewModel model = new
                (
                    VisitorCount: Random.Shared.Next(1, 1001),
                    Categories: await _db.Categories.ToListAsync(),
                    Products: await _db.Products.ToListAsync()
                );

        return View(model); // Pass the model to the view
    }


    
    /// <summary>
    /// This C# function retrieves product details based on the provided ID and displays an alert
    /// message with a specified style.
    /// </summary>
    /// <param name="id">The `id` parameter in the `ProductDetail` method is an integer representing the
    /// product ID. It is nullable (`int?`) to handle cases where the ID may not be provided.</param>
    /// <param name="alertstyle">The `alertstyle` parameter in the `ProductDetail` method is a string
    /// parameter with a default value of "success". This parameter is used to specify the style of an
    /// alert message that can be displayed in the view. The available styles could include "success",
    /// "info", "warning", "</param>
    /// <returns>
    /// The `ProductDetail` method returns an `IActionResult`. If the `id` parameter is null, it returns
    /// a BadRequest response with a message indicating that a product ID must be passed in the route.
    /// If a product with the specified ID is not found in the database, it returns a NotFound response
    /// with a message indicating that the product ID was not found. Otherwise, it returns a View with
    /// the
    /// </returns>
    public async Task<IActionResult> ProductDetail(int? id, string? alertstyle = "success")
    {
        ViewData["alertstyle"] = alertstyle;
        if (!id.HasValue)
        {
            return BadRequest("You must pass a product ID in the route, EX: /Home/ProductDetail/21");
        }

        Product? model = await _db.Products.Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.ProductId == id);

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
    [Route("privacy")]
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

    /// <summary>
    /// The ProductsThatCostMoreThan action method displays a list of products that cost more than a
    /// specified price.
    /// </summary>
    /// <param name="price">The price to filter products by. Must be a positive decimal number.</param>
    /// <returns>
    /// A view that displays the list of products that cost more than the specified price. If no
    /// products are found, a 404 not found response is returned. If the price parameter is not
    /// specified, a bad request response is returned.
    /// </returns>

    /// <summary>
    /// The function retrieves products from a database that cost more than a specified price and
    /// displays them in a view.
    /// </summary>
    /// <param name="price">The `ProductsThatCostMoreThan` method is an action method in a controller
    /// that retrieves products from a database where the unit price is greater than a specified
    /// price.</param>
    /// <returns>
    /// The `ProductsThatCostMoreThan` action in the controller is returning a view with a list of
    /// products that have a unit price higher than the specified price. If the price parameter is not
    /// provided in the route, a BadRequest response is returned with a message instructing the user to
    /// pass a price in the route. If there are no products that meet the criteria, a NotFound response
    /// is returned with a
    /// </returns>
    public IActionResult ProductsThatCostMoreThan(decimal? price)
    {
        if (!price.HasValue)
        {
            return BadRequest("You must pass a price in the route, for example: /Home/ProductsThatCostMoreThan?price=50");
        }

        IEnumerable<Product> model = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.UnitPrice > price);

        if (!model.Any())
        {
            return NotFound($"No products cost more than ${price:C}.");
        }

        ViewData["MaxPrice"] = price.Value.ToString("C");

        return View(model);
    }

    public async Task<IActionResult> CategoryDetail(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest("You must pass a category ID in the route, EX: /Home/CategoryDetail/21");
        }

        Category? model = await _db.Categories
            .Include(p => p.Products)
            .SingleOrDefaultAsync(p => p.CategoryId == id);
        
        if (model == null)
        {
            return NotFound($"No category with ID {id} was found.");
        }

        return View(model);
    }
}
