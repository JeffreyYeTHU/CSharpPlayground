using Microsoft.AspNetCore.Mvc;

namespace SaveLogStorage.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");
        _logger.LogInformation("The is a info log");

        return Enumerable.Range(1, 2).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private static Random random = new Random();

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}