using System;
using System.Collections.Generic;

namespace MysqlPerTest.Entity
{
    public partial class Course
    {
        public Course()
        {
            TestScores = new HashSet<TestScore>();
        }

        public int Id { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }

        public virtual ICollection<TestScore> TestScores { get; set; }
    }
}
