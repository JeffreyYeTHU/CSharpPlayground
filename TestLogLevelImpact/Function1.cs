using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TestLogLevelImpact
{
    public class Function1
    {
        [FunctionName("CallEfCore")]
        public async Task CallEfCore([TimerTrigger("* * * * * *", RunOnStartup = false)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("Function {Name} is called at {Time}", nameof(CallEfCore), DateTime.Now);
            for (int i = 0; i< 100; i++)
            {
                await AddAuthor();
                await UpdateAuthor();
                await RemoveAuthors();
            }
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
}
