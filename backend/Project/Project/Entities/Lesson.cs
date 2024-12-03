using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Lesson
    {
        public Lesson()
        {
            LessonPractices = new HashSet<LessonPractice>();
        }

        public string Id { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double NumberOfCredits { get; set; }
        public double NumberOfTheories { get; set; }
        public double NumberOfPractices { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SpecializationId { get; set; } = null!;
        public string? RoomId { get; set; }
        public int? LessonTypeId { get; set; }
        public decimal? OnlineLearningRate { get; set; }
        public string? RoomName { get; set; }
        public string? DepartmentId { get; set; }
        public string? SectionId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Room? Room { get; set; }
        public virtual Section? Section { get; set; }
        public virtual Specialization Specialization { get; set; } = null!;
        public virtual ICollection<LessonPractice> LessonPractices { get; set; }
    }
}
