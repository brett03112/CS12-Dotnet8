using Microsoft.AspNetCore.Mvc;
using Northwind.EntityModels;
using Northwind.WebApi.Repositories;

namespace Northwind.WebApi.Controllers;

// Base address:api/customers
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repo;

    public CustomerController(ICustomerRepository repo)
    {
        // Constructor injects repository registered in Program.cs
        _repo = repo;
    }

    // GET: api/customers
    // GET: api/customers/?country=[country]
    // This will always return a list of customers but it might be empty
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> GetCustomers(string? country)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            // Return all customers
            return await _repo.RetrieveAllAsync();
        }
        else
        {
            // Return customers from the specified country
            return (await _repo.RetrieveAllAsync())
                .Where(customer => customer.Country == country);
        }
        
    }

    //GET: api/customers/[id]
    [HttpGet("{id}", Name = nameof(GetCustomer))] // Named route
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        Customer? c = await _repo.RetrieveAsync(id);
        if (c is null)
        {
            return NotFound(); // 404
        }

        return Ok(c);

        /*
        This is a C# method in an ASP.NET Core Web API controller that handles 
        GET requests to retrieve a customer by ID. It returns the customer if 
        found (200 status code) or a 404 status code if not found.
        */
    }

    //POST: api/customers
    //BODY: Customer (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Customer c)
    {
        if (c == null)
        {
            return BadRequest(); // 400
        }

        Customer? addedCustomer = await _repo.CreateAsync(c);

        if (addedCustomer == null)
        {
            return BadRequest("Customer not added. Repository failed to create."); // 400
        }
        else
        {
            return CreatedAtRoute(
                routeName: nameof(GetCustomer),
                routeValues: new { id = addedCustomer.CustomerId.ToLower() },
                value: addedCustomer);
        }

    }

    // PUT: api/customers/[id]
    // BODY: Customer (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(string id, [FromBody] Customer c)
    {
        id = id.ToUpper();
        c.CustomerId = c.CustomerId.ToUpper();

        if (c == null || c.CustomerId != id)
        {
            return BadRequest(); // 400
        }

        Customer? existing = await _repo.RetrieveAsync(id);

        if (existing == null)
        {
            return NotFound(); // 404
        }

        await _repo.UpdateAsync(c);
        return new NoContentResult(); //204 

        
    }


    // DELETE: api/customers/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == "bad")
        {
            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://localhost:5151/customers/failed-to-delete",
                Title = $"Customer {id} was found but could not be deleted.",
                Detail = "More details like Company Name, Country, etc.",
                Instance = HttpContext.Request.Path
            };

            return BadRequest(problemDetails); // 400
        }

        Customer? existing = await _repo.RetrieveAsync(id);

        if (existing == null)
        {
            return NotFound(); // 404
        }

        bool? deleted = await _repo.DeleteAsync(id);

        if (deleted.HasValue && deleted.Value) // Short circuit AND
        {
            return new NoContentResult(); // 204
        }
        else
        {
            return BadRequest($"Customer {id} was found but could not be deleted."); // 400
        }
        
    }
}
