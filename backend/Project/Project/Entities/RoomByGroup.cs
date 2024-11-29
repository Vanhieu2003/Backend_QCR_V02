using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RoomByGroup
    {
        public string Id { get; set; } = null!;
        public string? RoomId { get; set; }
        public string? GroupRoomId { get; set; }
    }
}
