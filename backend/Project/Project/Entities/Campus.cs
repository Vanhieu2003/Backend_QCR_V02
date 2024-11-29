using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Campus
    {
        public string Id { get; set; } = null!;
        public string CampusCode { get; set; } = null!;
        public string CampusName { get; set; } = null!;
        public string? CampusName2 { get; set; }
        public string CampusSymbol { get; set; } = null!;
        public int SortOrder { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
