using Northwind.EntityModels;  // To use AddNorthwindContext method
using Microsoft.AspNetCore.Mvc.Formatters; // To use IOutputFormatter
using Microsoft.Extensions.Caching.Memory; // To use IMemoryCache
using Northwind.WebApi.Repositories; // To use CustomerRepository
using Swashbuckle.AspNetCore.SwaggerUI; // To use Swagger UI
using Microsoft.AspNetCore.HttpLogging; // TO use HttpLoggingField



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; // Default is 32k.
    options.ResponseBodyLogLimit = 4096; // Default is 32k.
});
/*
This line of code adds a singleton instance of IMemoryCache to the application's service 
container, using the MemoryCache class with default options.

In other words, it sets up a memory cache that can be used throughout the application, 
and the same instance will be shared across all requests.
*/
builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));


// Add services to the container.
builder.Services.AddNorthwindContext();

/*
This code snippet configures the output formatters for controllers in an ASP.NET Core application.

Here's what it does:

    It adds controllers to the application with a custom configuration.

    It prints the default output formatters to the console.

    For each output formatter, it checks if it's an instance of OutputFormatter (which is a specific 
    type of formatter that supports media types).

    If it is, it prints the formatter's type name and its supported media types.

    Finally, it adds two additional output formatters: XmlDataContractSerializerFormatters and 
    XmlSerializerFormatters, which enable XML serialization for the application.

In summary, this code sets up the output formatters for the application, including XML serialization, and 
prints the default formatters to the console for debugging purposes.
*/
builder.Services.AddControllers(options =>
{
    WriteLine("Default output formatters:");
    foreach (IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if (mediaFormatter == null)
        {
            WriteLine($" {formatter.GetType().Name}");
        }
        else
        {
            WriteLine(" {0}, Media type: {1}",
            arg0: mediaFormatter.GetType().Name,
            arg1: string.Join(", ", mediaFormatter.SupportedMediaTypes));
        }
    }
})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/*
This line of code registers the CustomerRepository class as the implementation of the 
ICustomerRepository interface in the application's service container, with a scoped lifetime.

In other words, it tells the application to create a new instance of CustomerRepository for each 
HTTP request, and to use that instance to fulfill all requests for ICustomerRepository within the 
scope of that request.

This is based on the context from Repositories/CustomerRepository.cs and Repositories/ICustomerRepository.cs.
*/
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
 {
     c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "Northwind Service API Version 1");

     c.SupportedSubmitMethods(new[] {
      SubmitMethod.Get, SubmitMethod.Post,
      SubmitMethod.Put, SubmitMethod.Delete });
 });
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.Run();
