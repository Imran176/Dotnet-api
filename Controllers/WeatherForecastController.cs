
// Import ASP.NET Core MVC functionality.
// This provides attributes such as ApiController, Route, and HttpGet.
using Microsoft.AspNetCore.Mvc;

// Define the namespace for organizing API controllers.
namespace DotnetApi.Controllers;

// Mark this class as an API controller.
// ASP.NET Core automatically enables API-specific behaviors.
[ApiController]

// Define the route using the controller's name.
// For WeatherForecastController, the URL becomes /WeatherForecast.
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // Store possible weather descriptions in an array.
    // A random description will be selected for each forecast.
    private readonly string[] _summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // Handle HTTP GET requests sent to /WeatherForecast.
    // The method returns a collection of five weather forecasts.
    [HttpGet("", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> GetFiveDayForecast()
    {
        // Generate a sequence of five numbers (1 through 5).
        // Each number represents a forecast day.
        var forecast = Enumerable.Range(1, 5).Select(index =>

            // Create a new WeatherForecast object for each day.
            new WeatherForecast
            (
                // Calculate the forecast date by adding the index
                // to the current date.
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),

                // Generate a random temperature between -20 and 54 Celsius.
                Random.Shared.Next(-20, 55),

                // Select a random weather description from the summaries array.
                _summaries[Random.Shared.Next(_summaries.Length)]
            )
        )
        // Convert the generated forecasts into an array.
        .ToArray();

        // Return the five-day weather forecast.
        // ASP.NET Core automatically serializes it into JSON.
        return forecast;
    }
}

// Define a record to represent a weather forecast.
// A record is a convenient way to store data.
public record WeatherForecast(
    DateOnly Date,       // Date of the forecast.
    int TemperatureC,    // Temperature in Celsius.
    string? Summary      // Weather description (can be null).
)
{
    // Calculate the temperature in Fahrenheit from Celsius.
    // This property is automatically included in the JSON response.
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
