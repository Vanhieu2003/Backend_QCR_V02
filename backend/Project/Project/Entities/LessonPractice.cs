using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class LessonPractice
    {
        public string Id { get; set; } = null!;
        public string LessonId { get; set; } = null!;
        public string LessonCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double NumberOfPeriods { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PracticeType { get; set; }
        public double? NumberOfStudentRegistered { get; set; }
        public int? StartPeriods { get; set; }
        public int? EndPeriods { get; set; }

        public virtual Lesson Lesson { get; set; } = null!;
    }
}
