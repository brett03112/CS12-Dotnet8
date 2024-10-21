#region Import Namespaces
using Northwind.EntityModels; // To use AddNorthwindContext method
using Microsoft.AspNetCore.Identity; // To use IdentityUser
using Microsoft.EntityFrameworkCore; // To use DbContext
using Northwind.Mvc.Data; // To use ApplicationDbContext
#endregion

#region Configure the host web server including services
/*
The second section creates and configures a web host builder that does the following:

    • Registers an application database context using SQL Server or SQLite. The database
    connection string is loaded from the appsettings.json file.

    • Adds ASP.NET Core Identity for authentication and configures it to use the application
    database.

    • Adds support for MVC controllers with views and Razor pages.
*/
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddNorthwindContext(); // To use AddNorthwindContext method

var app = builder.Build();

#endregion

#region Configure the HTTP request pipeline

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

/*
***example URLs and how the default route would work out the names of a controller and action:

• Specific Razor view: /Views/{controller}/{action}.cshtml

• Shared Razor view: /Views/Shared/{action}.cshtml

• Shared Razor Page: /Pages/Shared/{action}.cshtml


        URL                     Controller      Action      ID

        /                       Home            Index

        /Muppet                 Muppet          Index

        /Muppet/Kermit          Muppet          Kermit

        /Muppet/Kermit/Green    Muppet          Kermit      Green

        /Products               Products        Index

        /Products/Detail        Products        Detail

        /Products/Detail/3      Products        Detail      3


***ControllerBase has many useful properties for working with the current HTTP context, as shown in Table 3: ***
Property            Description

Request             Just the HTTP request, for example, headers, query string parameters, the body of
                    the request as a stream that you can read from, the content type and length, and
                    cookies.

Response            Just the HTTP response, for example, headers, the body of the response as a stream
                    that you can write to, the content type and length, status code, and cookies. There
                    are also delegates like OnStarting and OnCompleted that you can hook a method up
                    to.

HttpContext         Everything about the current HTTP context, including the request and response,
                    information about the connection, a collection of features that have been enabled on
                    the server with middleware, and a User object for authentication and authorization.


***ControllerBase properties***

Property            Description

ViewData            A dictionary in which the controller can store key/value pairs that is accessible in a view.
                    The dictionary’s lifetime is only for the current request/response.

ViewBag             A dynamic object that wraps the ViewData to provide a friendlier syntax for setting and
                    getting dictionary values.

TempData            A dictionary in which the controller can store key/value pairs that is accessible in a view.
                    The dictionary’s lifetime is for the current request/response and the next request/response
                    for the same visitor session. This is useful for storing a value during an initial request,
                    responding with a redirect, and then reading the stored value in the subsequent request.

***Controller methods***


Method              Description

View                Returns a ViewResult after executing a view that renders a full response, for
                    example, a dynamically generated web page. The view can be selected using a
                    convention or be specified with a string name. A model can be passed to the view.

PartialView         Returns a PartialViewResult after executing a view that is part of a full response,
                    for example, a dynamically generated chunk of HTML. The view can be selected
                    using a convention or be specified with a string name. A model can be passed to the
                    view.

ViewComponent       Returns a ViewComponentResult after executing a component that dynamically
                    generates HTML. The component must be selected by specifying its type or its
                    name. An object can be passed as an argument.

Json                Returns a JsonResult containing a JSON-serialized object. This can be useful for
                    implementing a simple Web API as part of an MVC controller that primarily returns
                    HTML for a human to view.


***Responsibility of MVC controllers***

The responsibilities of a controller are as follows:

• Identify the services that the controller needs to be in a valid state and to function properly
in their class constructor(s).

• Use the action name to identify a method to execute.

• Extract parameters from the HTTP request.

• Use the parameters to fetch any additional data needed to construct a view model and pass it to
the appropriate view for the client. For example, if the client is a web browser, then a view that
renders HTML would be most appropriate. Other clients might prefer alternative renderings,
like document formats such as a PDF file or an Excel file, or data formats, like JSON or XML.

• Return the results from the view to the client as an HTTP response with an appropriate status
code.
*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //.WithStaticAssets();
/*
If the visitor navigates to a path of / or /Home, then it is the equivalent of /Home/Index because those
were the default names for the controller and action in the default route.
*/


app.MapRazorPages()
    .WithStaticAssets();

#endregion

#region Start the host web server listening for HTTP requests

app.Run(); // This is a blocking call and will not return until the web server is shut down

#endregion