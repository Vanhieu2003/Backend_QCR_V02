using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class UserPerResGroup
    {
        public string Id { get; set; } = null!;
        public string? ResponsiableGroupId { get; set; }
        public string? UserId { get; set; }
    }
}
