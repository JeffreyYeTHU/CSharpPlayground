using System;
using System.Collections.Generic;

namespace MysqlPerTest.Entity
{
    public partial class TestScore
    {
        public int Id { get; set; }
        public int StudientId { get; set; }
        public int CourseId { get; set; }
        public double Score { get; set; }

        public virtual Course Course { get; set; }
        public virtual Studient Studient { get; set; }
    }
}
