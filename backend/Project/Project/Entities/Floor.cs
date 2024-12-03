using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Floor
    {
        public string Id { get; set; } = null!;
        public string FloorCode { get; set; } = null!;
        public string FloorName { get; set; } = null!;
        public string? Description { get; set; }
        public int FloorOrder { get; set; }
        public int BasementOrder { get; set; }
        public int SortOrder { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
