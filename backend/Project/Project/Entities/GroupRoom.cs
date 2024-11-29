using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class GroupRoom
    {
        public string Id { get; set; } = null!;
        public string? GroupName { get; set; }
        public string? Description { get; set; }
    }
}
