using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Event
    {
        public string Id { get; set; } = null!;
        public string? Title { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool AllDay { get; set; }
        public bool Cancelled { get; set; }
        public string? EventTypeId { get; set; }
        public string? ScheduleId { get; set; }

        public virtual RoomSchedule? Schedule { get; set; }
    }
}
