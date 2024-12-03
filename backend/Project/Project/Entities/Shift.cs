using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Shift
    {
        public string Id { get; set; } = null!;
        public string ShiftName { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? RoomCategoryId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Status { get; set; }
    }
}
