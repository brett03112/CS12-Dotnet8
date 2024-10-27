using Microsoft.AspNetCore.Mvc;

namespace Northwind.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    /// <summary>
    /// Constructor that takes an <see cref="ILogger{WeatherForecastController}"/> to
    /// log information about the execution of the controller.
    /// </summary>
    /// <param name="logger">The logger to use for logging messages.</param>
/// 
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Retrieves weather forecasts for the next 5 days.
    /// </summary>
    /// <returns>The weather forecasts for the next 5 days.</returns>
    [HttpGet(Name = "GetWeatherForecastFiveDays")]
    public IEnumerable<WeatherForecast> GetF()
    {
        return Get(days: 5);
    }

    /// <summary>
    /// Retrieves weather forecasts for the specified number of days.
    /// </summary>
    /// <param name="days">The number of days of weather forecasts to retrieve.</param>
    /// <returns>The weather forecasts for the specified number of days.</returns>
    [HttpGet(template: "{days:int}", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get(int days)
    {
        return Enumerable.Range(1, days).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
