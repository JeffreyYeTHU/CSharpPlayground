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
        private readonly PerfTestContext _dbContext;

        public DataWriteController(PerfTestContext dbContext)
        {
            _dbContext = dbContext;
        }

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

            int studientCnt = 20_000_000;
            int nextStudientId = _dbContext.Studients.Count() == 0 ? 1 :  _dbContext.Studients.Select(s => s.Id).Max() + 1;
            int nextScoreId = _dbContext.TestScores.Count() == 0 ? 1 : _dbContext.TestScores.Select(s =>s.Id).Max() + 1;
            for (int i = 0; i < studientCnt; i++)
            {
                // studient
                var studient = AutoFaker.Generate<Studient>();
                int year = Random.Shared.Next(1985, 1995);
                int month = Random.Shared.Next(1, 12);
                int day;
                if (month == 2)
                    day = Random.Shared.Next(1, 28);
                else
                    day = Random.Shared.Next(1, 30);
                studient.Birthday = new DateOnly(year, month, day);
                string[] gender = new string[] { "Male", "Famale" };
                studient.Gender = gender[Random.Shared.Next(0, 1)];
                studient.Id = nextStudientId;
                nextStudientId++;
                _dbContext.Studients.Add(studient);

                // score, foreach studient, make random corse scores
                HashSet<int> courseIds = new();
                for (int j = 0; j < 4; j++)
                    courseIds.Add(Random.Shared.Next(1, 10));
                foreach (var courseId in courseIds)
                {
                    var testScore = new TestScore()
                    {
                        Id = nextScoreId,
                        StudientId = studient.Id,
                        CourseId = courseId,
                        Score = Random.Shared.Next(30, 100)
                    };
                    nextScoreId++;
                    _dbContext.TestScores.Add(testScore);
                }

                // save to db every 1000 items
                if( i % 1000 == 0)
                    await _dbContext.SaveChangesAsync();
            }
        }

        [HttpPost("write_course_data")]
        public async Task WriteCouseData()
        {
            for(int i = 1; i <= 10; i++)
            {
                var course = new Course
                {
                    Id = i,
                    CourseName = "course" + i,
                    TeacherName = "teacher" + i
                };
                _dbContext.Courses.Add(course);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
