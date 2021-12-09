using System;
using System.Collections.Generic;

namespace MysqlPerTest.Entity
{
    public partial class Studient
    {
        public Studient()
        {
            TestScores = new HashSet<TestScore>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly Birthday { get; set; }
        public string Gender { get; set; }

        public virtual ICollection<TestScore> TestScores { get; set; }
    }
}
