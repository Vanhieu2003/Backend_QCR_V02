using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class EventType
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public short? Status { get; set; }
        public int? Code { get; set; }
    }
}
