using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Repair
    {
        public string Id { get; set; } = null!;
        public string? RequestId { get; set; }
        public string? Reason { get; set; }
        public int? RepairMethod { get; set; }
        public string? Description { get; set; }
        public string? Supplier { get; set; }
        public int? RepairCost { get; set; }
    }
}
