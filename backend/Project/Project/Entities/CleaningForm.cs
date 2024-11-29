using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class CleaningForm
    {
        public string Id { get; set; } = null!;
        public string FormName { get; set; } = null!;
        public string? RoomId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
