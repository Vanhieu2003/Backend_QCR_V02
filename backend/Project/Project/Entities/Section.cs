using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Section
    {
        public Section()
        {
            Lessons = new HashSet<Lesson>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string DepartmentId { get; set; } = null!;

        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
