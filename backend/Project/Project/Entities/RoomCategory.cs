using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RoomCategory
    {
        public RoomCategory()
        {
            Rooms = new HashSet<Room>();
        }

        public string Id { get; set; } = null!;
        public string? CategoryCode { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
