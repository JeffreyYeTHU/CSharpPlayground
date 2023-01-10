using Microsoft.AspNetCore.Mvc;
using System.Text;

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
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        //_logger.LogInformation("This method is call at {Time} with num = {Num}");

        var sb1 = new StringBuilder();
        var sb2 = new StringBuilder();
        var sb3 = new StringBuilder();
        var sb4 = new StringBuilder();
        for (int i=0; i<1000000; i++)
        {
            var num = random.Next(200, 20000).ToString();
            //var t = DateTime.Now.ToString();
            sb1.AppendLine("This method is call with num = {Num}");
            sb2.AppendLine(num);
            //sb3.AppendLine(t);
            sb4.AppendLine($"This method is call with num = {num}");
        }
        await System.IO.File.WriteAllTextAsync("log-structure-template.txt", sb1.ToString());
        await System.IO.File.WriteAllTextAsync("log-structure-parm1.txt", sb2.ToString());
        //await System.IO.File.WriteAllTextAsync("log-structure-parm1.txt", sb3.ToString());
        await System.IO.File.WriteAllTextAsync("log-non-structure.txt", sb4.ToString());

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