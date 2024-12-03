using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Item
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public string? GroupCode { get; set; }
        public string? Specification { get; set; }
        public string? Trademark { get; set; }
    }
}
