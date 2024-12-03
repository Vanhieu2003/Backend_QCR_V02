using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class CleaningReport
    {
        public string Id { get; set; } = null!;
        public string? FormId { get; set; }
        public int? Value { get; set; }
        public string UserId { get; set; } = null!;
        public string ShiftId { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
