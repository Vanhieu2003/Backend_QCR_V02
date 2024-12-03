using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class CriteriaTag
    {
        public string Id { get; set; } = null!;
        public string? CriteriaId { get; set; }
        public string? TagId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
