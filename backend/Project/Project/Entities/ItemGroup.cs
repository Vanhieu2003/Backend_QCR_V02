using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class ItemGroup
    {
        public ItemGroup()
        {
            LessonPracticeQuota = new HashSet<LessonPracticeQuotum>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public virtual ICollection<LessonPracticeQuotum> LessonPracticeQuota { get; set; }
    }
}
