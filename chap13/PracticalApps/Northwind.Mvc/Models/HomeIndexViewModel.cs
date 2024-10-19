using Northwind.EntityModels;

/*
Good Practice: Although the ErrorViewModel class created by the MVC project
template does not follow this convention, I recommend that you use the naming
convention {Controller}{Action}ViewModel for your view model classes.
*/

namespace Northwind.Mvc.Models;

public record HomeIndexViewModel(int VisitorCount, IList<Category> Categories, IList<Product> Products);
