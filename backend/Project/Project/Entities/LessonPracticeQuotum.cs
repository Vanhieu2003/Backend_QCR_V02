using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class LessonPracticeQuotum
    {
        public string Id { get; set; } = null!;
        public string LessonPracticeId { get; set; } = null!;
        public string? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemGroupId { get; set; }
        public string? ItemGroupCode { get; set; }
        public string? Unit { get; set; }
        public decimal? Depreciation { get; set; }
        public decimal? CompleteQuantification { get; set; }
        public int? NumberOfStudents { get; set; }
        public decimal? Quota { get; set; }
        public string? Result { get; set; }

        public virtual ItemGroup? ItemGroup { get; set; }
    }
}
