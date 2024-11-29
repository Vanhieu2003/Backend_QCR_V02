using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FloorOfBlock
    {
        public string Id { get; set; } = null!;
        public string? FloorId { get; set; }
        public string? BlockId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
