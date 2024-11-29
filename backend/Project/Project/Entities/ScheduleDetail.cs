using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class ScheduleDetail
    {
        public string Id { get; set; } = null!;
        public string? ScheduleId { get; set; }
        public string? RoomId { get; set; }
        public string? UserId { get; set; }
        public string? RoomType { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
