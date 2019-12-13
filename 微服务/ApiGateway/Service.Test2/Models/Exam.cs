using System;
using System.Collections.Generic;

namespace Service.Test2.Models
{
    public partial class Exam
    {
        public int StuId { get; set; }
        public decimal? WriteExam { get; set; }
        public int ExamNo { get; set; }
        public decimal? LadExam { get; set; }
    }
}
