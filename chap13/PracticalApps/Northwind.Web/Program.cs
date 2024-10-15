using Northwind.EntityModels; // To add NorthwindContext method
using Microsoft.AspNetCore.Server.Kestrel.Core; // To use HttpProtocols
using static System.Console;

#region Configure the web server host and services

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext(); // To add NorthwindContext method


builder.Services.AddRequestDecompression(); // Added to support HTTP/3

// Added to support HTTP/3. In appsettings.json added: "Microsoft.AspNetCore.Hosting.Diagnostics": "Information"
/*
Configuring Kestrel to Support HTTP/3

This code snippet configures the Kestrel web server to support HTTP/3, in addition to HTTP/1 and HTTP/2. 
It also enables HTTPS, which is required for HTTP/3.

Here's a breakdown:

builder.WebHost.ConfigureKestrel:   Configures the Kestrel web server.

options.ConfigureEndpointDefaults:  Sets default options for all endpoints.
listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3:  Enables support for HTTP/1, HTTP/2, and HTTP/3.

listenOptions.UseHttps():           Enables HTTPS, which is required for HTTP/3.

By setting these options, the web server will be able to handle requests using all three HTTP 
versions and will require secure connections for HTTP/3.
*/
builder.WebHost.ConfigureKestrel((context, options) =>
{
  options.ConfigureEndpointDefaults(listenOptions =>
  {
    listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
    listenOptions.UseHttps(); // HTTP/3 requires secure connections.
  });
});

var app = builder.Build();

#endregion


#region Configure the HTTP pipeline and routes

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

/*
Middleware delegates are configured using one of the following methods or a custom method that
calls them itself:
        • Run: Adds a middleware delegate that terminates the pipeline by immediately returning a
        response instead of calling the next middleware delegate.

        • Map: Adds a middleware delegate that creates a branch in the pipeline when there is a matching
        request usually based on a URL path like /hello.

        • Use: Adds a middleware delegate that forms part of the pipeline so it can decide if it wants to
        pass the request to the next delegate in the pipeline. It can modify the request and response
        before and after the next delegate.

For convenience, there are many extension methods that make it easier to build the pipeline, for
example, UseMiddleware<T>, where T is a class that has:

        • A constructor with a RequestDelegate parameter that will be passed to the next pipeline
        component.

        • An Invoke method with an HttpContext parameter and returns a Task.

Key middleware extension methods used in our code include the following:

        • UseHsts: Adds middleware for using HSTS, which adds the Strict-Transport-Security header.

        • UseHttpsRedirection: Adds middleware for redirecting HTTP requests to HTTPS, so in our
        code a request for http://localhost:5130 would receive a 307 response telling the browser
        to request https://localhost:5131.

        • UseDefaultFiles: Adds middleware that enables default file mapping on the current path, so
        in our code it would identify files such as index.html or default.html.

        • UseStaticFiles: Adds middleware that looks in wwwroot for static files to return in the HTTP
        response.

        • MapRazorPages: Adds middleware that will map URL paths such as /suppliers to a Razor Page
        file in the /Pages folder named suppliers.cshtml and return the results as the HTTP response.

        • MapGet: Adds middleware that will map URL paths such as /hello to an inline delegate that
        writes plain text directly to the HTTP response.

If we had chosen a different project template that supports more complex routing scenarios, for
example, the ASP.NET Core MVC website project template, then we would have seen other common
middleware extension methods, which include the following:

        • UseRouting: Adds middleware that defines a point in the pipeline where routing decisions are
        made and must be combined with a call to UseEndpoints where the processing is then executed.

        • UseEndpoints: Adds middleware to execute to generate responses from decisions made earlier
        in the pipeline.
*/

// Implementing an anonymous inline delegate as middleware to intercept HTTP requests and responses
app.Use(async (HttpContext context, Func<Task> next) =>
{
    RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;

    if (rep is not null)
    {
        WriteLine($"Endpoint name: {rep.DisplayName}");
        WriteLine($"Endpoint route pattern: {rep.RoutePattern.RawText}");   
    }

    if (context.Request.Path == "/bonjour")
    {
        // In the case of a match on URL path, this becomes a terminating delegate
        // that returns so does not call the next delegate in the pipeline
        await context.Response.WriteAsync("Bonjour le monde!");
        return;
    }

    // We could modify the request before calling the next delegate
    await next();

    // We could modify the response after calling the next delegate
});

app.UseHttpsRedirection();

app.UseRequestDecompression();

app.UseDefaultFiles(); // Must come before UseStaticFiles! index.html default.html etc.
app.UseStaticFiles();

app.MapRazorPages();
app.MapGet("/hello", () => $"Environment is {app.Environment.EnvironmentName}");

#endregion

app.Run();


WriteLine("\n\n\n\nThis executes after the web server has stopped!");   

/*
The web application builder registers services that can then be retrieved when the functionality
they provide is needed using dependency injection. The naming convention for a method that registers a service is AddService where Service is the service name, for example, AddRazorPages or
AddNorthwindContext. Our code registers two services: Razor Pages and an EF Core database context.
Common methods that register dependency services, including services that combine other method
calls that register services:

Method                          Services that it registers

AddMvcCore                      Minimum set of services necessary to route requests and invoke
                                controllers. Most websites will need more configuration than this.

AddAuthorization                Authentication and authorization services.

AddDataAnnotations              MVC data annotations service.

AddCacheTagHelper               MVC cache tag helper service.

AddRazorPages                   Razor Pages service, including the Razor view engine. Commonly used
                                in simple website projects. It calls the following additional methods:
                                    AddMvcCore
                                    AddAuthorization
                                    AddDataAnnotations
                                    AddCacheTagHelper

AddApiExplorer                  Web API explorer service.

AddCors                         Cross-origin resource sharing (CORS) support for enhanced security.

AddFormatterMappings            Mappings between a URL format and its corresponding media type.

AddControllers                  Controller services but not services for views or pages. Commonly used
                                in ASP.NET Core Web API projects. It calls the following additional
                                methods:
                                    AddMvcCore
                                    AddAuthorization
                                    AddDataAnnotations
                                    AddCacheTagHelper
                                    AddApiExplorer
                                    AddCors
                                    AddFormatterMappings

AddViews                        Support for .cshtml views including default conventions.

AddRazorViewEngine              Support for the Razor view engine including processing the @ symbol.

AddControllersWithViews         Controller, view, and page services. Commonly used in ASP.NET Core
                                MVC website projects. It calls the following additional methods:
                                    AddMvcCore
                                    AddAuthorization
                                    AddDataAnnotations
                                    AddCacheTagHelper
                                    AddApiExplorer
                                    AddCors
                                    AddFormatterMappings
                                    AddViews
                                    AddRazorViewEngine

AddMvc                          Similar to AddControllersWithViews, but you should only use it for
                                backward compatibility.

AddDbContext<T>                 Your DbContext type and its optional DbContextOptions<TContext>.

AddNorthwindContext             A custom extension method we created to make it easier to register the
                                NorthwindContext class for either SQLite or SQL Server based on the
                                project referenced.


*/
