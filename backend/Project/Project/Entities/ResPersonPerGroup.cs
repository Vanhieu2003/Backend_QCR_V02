using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class ResPersonPerGroup
    {
        public string Id { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? ResponsibleLpersonId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
