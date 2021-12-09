using AutoBogus;
using AutoBogus.Conventions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MysqlPerTest.Entity;

namespace MysqlPerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataWriteController : ControllerBase
    {
        //private readonly PerfTestContext context;
        private readonly IServiceProvider service;
        private readonly ILogger<DataWriteController> logger;

        public DataWriteController(IServiceProvider service, ILogger<DataWriteController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        //public DataWriteController(PerfTestContext context)
        //{
        //    this.context = context;
        //}

        [HttpPost("write_studient_data")]
        public async Task WriteStudientData()
        {
            AutoFaker.Configure(builder =>
            {
                builder.WithSkip<Studient>(s => s.Id);
                builder.WithSkip<Studient>(s => s.Birthday);
                builder.WithSkip<Studient>(s => s.Gender);
                builder.WithSkip<Studient>(s => s.TestScores);
                builder.WithConventions();
            });

            var context = service.GetService<PerfTestContext>();
            if (context == null)
                throw new Exception("Cannot get dbContext");
            int startStudientId = !context.Studients.Any() ? 1 : context.Studients.Select(s => s.Id).Max() + 1;
            int startScoreId = !context.TestScores.Any() ? 1 : context.TestScores.Select(s => s.Id).Max() + 1;
            await context.DisposeAsync();

            // Let's make 5 concurrent task, and write 1000 each task
            for(int r = 0; r < 50; r++)
            {
                int taskCnt = 20;
                int recordOfSingleTask = 1000;
                startStudientId += r * taskCnt * recordOfSingleTask;
                startScoreId += startStudientId * 3;
                var tesks = Enumerable.Range(0, taskCnt)
                    .Select(i =>
                    {
                        int currStart = startStudientId + i * recordOfSingleTask;
                        return WriteSetOfStudient(currStart, currStart + recordOfSingleTask, startScoreId + currStart * 3);
                    });
                await Task.WhenAll(tesks);
            }
        }

        private async Task WriteSetOfStudient(int startStudientId, int endStudientId, int startScoreId)
        {
            //logger.LogInformation(
            //    "Trying to write studients from startStudientId = {startStudientId}, endStudientId = {endStudientId - 1}, startScoreId = {startScoreId}",
            //    startStudientId, endStudientId, startScoreId);
            var context = service.GetService<PerfTestContext>();
            if (context == null)
                throw new Exception("Cannot get dbContext");
            for (int i = startStudientId; i < endStudientId; i++)
            {
                // studient
                var studient = AutoFaker.Generate<Studient>();
                int year = Random.Shared.Next(1985, 1995);
                int month = Random.Shared.Next(1, 13);
                int day;
                if (month == 2)
                    day = Random.Shared.Next(1, 28);
                else
                    day = Random.Shared.Next(1, 31);
                studient.Birthday = new DateOnly(year, month, day);
                string[] gender = new string[] { "Male", "Famale" };
                studient.Gender = gender[Random.Shared.Next(0, 2)];
                studient.Id = i;
                context.Studients.Add(studient);

                // score, foreach studient, make random corse scores
                HashSet<int> courseIds = new();
                for (int j = 0; j < 3; j++)
                    courseIds.Add(Random.Shared.Next(1, 11));
                foreach (var courseId in courseIds)
                {
                    var testScore = new TestScore()
                    {
                        Id = startScoreId,
                        StudientId = studient.Id,
                        CourseId = courseId,
                        Score = Random.Shared.Next(30, 100)
                    };
                    startScoreId++;
                    context.TestScores.Add(testScore);
                }
            }
            await context.SaveChangesAsync();
            await context.DisposeAsync();
        }
    }
}
