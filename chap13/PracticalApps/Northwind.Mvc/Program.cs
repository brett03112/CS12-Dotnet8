#region Import Namespaces
using Northwind.EntityModels; // To use AddNorthwindContext method
using Microsoft.AspNetCore.Identity; // To use IdentityUser
using Microsoft.EntityFrameworkCore; // To use DbContext
using Northwind.Mvc.Data;
using Microsoft.Extensions.Options; // To use ApplicationDbContext
using System.Net.Http.Headers; // To use MediaTypeWithQualityHeaderValue
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
/* The line `var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");` is
retrieving the connection string named "DefaultConnection" from the configuration settings. */
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


/* `builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));` is configuring and adding a database context to the
services container in the ASP.NET Core application. */
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));


/* `builder.Services.AddDatabaseDeveloperPageExceptionFilter();` is adding a developer exception page
filter to the services container in the ASP.NET Core application. This filter is specifically
designed for database-related exceptions that occur during development. */
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


/* `builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount
= true)
    .AddEntityFrameworkStores<ApplicationDbContext>();` */
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


/* `builder.Services.AddControllersWithViews();` is adding support for MVC controllers with views to
the services container in the ASP.NET Core application. This method registers the necessary services
for handling controllers and views, allowing the application to respond to HTTP requests by
rendering views and returning HTML responses. */
builder.Services.AddControllersWithViews();


/* `builder.Services.AddNorthwindContext();` is a custom method being called to add the necessary
services related to the Northwind database context to the services container in the ASP.NET Core
application. This method likely configures and registers services required to interact with the
Northwind database, such as setting up the database context, entity framework configurations, and
any other dependencies specific to the Northwind database within the application. */
builder.Services.AddNorthwindContext(); // To use AddNorthwindContext method


/* The below code is configuring output caching in a C# application. It is using the `AddOutputCache`
method to set up caching options. */
builder.Services.AddOutputCache(options =>
{
    /* The below code in C# is setting the default expiration time span to 10 seconds. This means that
    any cached items will expire and be removed from the cache after 10 seconds. */
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(10);


    /* The below code is setting a caching policy named "views" with the option to vary the cache by
    query parameters. This means that the cached response will be different based on the query
    parameters in the request URL. */
    options.AddPolicy("views", p => p.SetVaryByQuery("alertstyle"));
});

/* The below code is configuring an HttpClient named "Northwind.WebApi" in a C# application. It sets
the base address of the HttpClient to "https://localhost:5151/" and adds a default request header to
accept JSON content with a quality value of 1.0. This HttpClient configuration can be used to make
HTTP requests to the specified base address with the specified headers. */
builder.Services.AddHttpClient(name: "Northwind.WebApi",
    configureClient: options => 
    {
        options.BaseAddress = new Uri("https://localhost:5151/");
        options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(
            mediaType:"application/json", quality: 1.0));
    });

var app = builder.Build();

#endregion

#region Configure the HTTP request pipeline

// Configure the HTTP request pipeline.
/* The code snippet `if (app.Environment.IsDevelopment())` is checking the environment in which the
application is running. If the application is running in a development environment, it will execute
`app.UseMigrationsEndPoint();`. This method adds middleware to enable the endpoint for applying
migrations to the database during development. */
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


/* `app.UseHttpsRedirection();` is a middleware in ASP.NET Core that redirects HTTP requests to HTTPS.
When this middleware is added to the request pipeline, it checks if the incoming request is using
HTTP. If it is, the middleware automatically issues a redirect response to the same URL but with
HTTPS instead of HTTP. */
app.UseHttpsRedirection();


/* `app.UseRouting();` is a middleware in ASP.NET Core that sets up the routing for incoming HTTP
requests. This middleware is responsible for inspecting the incoming request's URL and determining
which endpoint (controller action) should handle the request based on the configured routes in the
application. */
app.UseRouting();


/* `app.UseAuthorization();` is a middleware in ASP.NET Core that adds authorization policy evaluation
to the request pipeline. This middleware is responsible for checking whether the current user is
authorized to access the requested resource based on the defined authorization policies in the
application. */
app.UseAuthorization();


/* `app.MapStaticAssets();` is likely a custom method or extension method being called in the ASP.NET
Core application to map and serve static assets such as CSS, JavaScript, images, or other files that
do not require server-side processing. */
app.MapStaticAssets();


/* `app.UseOutputCache();` is likely a custom middleware or extension method being called in the
ASP.NET Core application to enable output caching. */
app.UseOutputCache();

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
    //.CacheOutput(policyName: "views");
    /* The above code in C# is setting up a default route for controllers in an application. It specifies
that if no specific controller or action is provided in the URL, it will default to the "Home"
controller and the "Index" action. The "id" parameter is optional. Additionally, the code is using
the `CacheOutput` method to apply caching with a policy named "views" to the route. */

/*
If the visitor navigates to a path of / or /Home, then it is the equivalent of /Home/Index because those
were the default names for the controller and action in the default route.
*/


/* The code `app.MapRazorPages().WithStaticAssets();` is configuring the ASP.NET Core application to
map Razor Pages and serve static assets. */
app.MapRazorPages()
    .WithStaticAssets();


/* The line `app.MapGet("/notcached", () => DateTime.Now.ToString());` is configuring a route in the
ASP.NET Core application for handling HTTP GET requests to the path "/notcached". */
app.MapGet("/notcached", () => DateTime.Now.ToString());


/* The line `app.MapGet("/cached", () => DateTime.Now.ToString()).CacheOutput();` is configuring a
route in the ASP.NET Core application for handling HTTP GET requests to the path "/cached" and
caching the output of the request. */
app.MapGet("/cached", () => DateTime.Now.ToString()).CacheOutput();

#endregion

#region Start the host web server listening for HTTP requests

app.Run(); // This is a blocking call and will not return until the web server is shut down

#endregion