using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Specialization
    {
        public Specialization()
        {
            Lessons = new HashSet<Lesson>();
            Departments = new HashSet<Department>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double NumberOfCredits { get; set; }
        public double NumberOfPeriods { get; set; }
        public double NumberOfPractices { get; set; }
        public string? Code { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
