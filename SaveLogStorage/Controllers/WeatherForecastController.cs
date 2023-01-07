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
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        _logger.LogInformation("This is an info log");
        await AddAuthor();
        await UpdateAuthor();
        await RemoveAuthors();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private async Task AddAuthor()
    {
        using var db = new DemoDbContext();
        db.Database.EnsureCreated();
        var authors = new List<Author>
            {
                new Author
                {
                    FirstName ="Joydip",
                    LastName ="Kanjilal",
                    Books = new List<Book>
                    {
                        new Book { Title = "Mastering C# 8.0"},
                        new Book { Title = "Entity Framework Tutorial"},
                        new Book { Title = "ASP.NET 4.0 Programming"}
                    }
                },
                new Author
                {
                    FirstName ="Yashavanth",
                    LastName ="Kanetkar",
                    Books = new List<Book>()
                    {
                        new Book { Title = "Let us C"},
                        new Book { Title = "Let us C++"},
                        new Book { Title = "Let us C#"}
                    }
                }
            };
        db.Authors.AddRange(authors);
        await db.SaveChangesAsync();
    }

    private async Task UpdateAuthor()
    {
        using var db = new DemoDbContext();
        db.Database.EnsureCreated();
        var author = db.Authors.Where(author => author.FirstName == "Yashavanth").First();
        author.FirstName = "YASHAVANTH";
        db.Authors.Update(author);
        await db.SaveChangesAsync();
    }

    private async Task RemoveAuthors()
    {
        using var db = new DemoDbContext();
        db.Database.EnsureCreated();
        var authors = db.Authors.Where(author => author.FirstName != null).ToList();
        db.RemoveRange(authors);
        await db.SaveChangesAsync();
    }
}

