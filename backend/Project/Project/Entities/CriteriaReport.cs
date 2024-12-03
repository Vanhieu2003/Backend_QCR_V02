using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class CriteriaReport
    {
        public string Id { get; set; } = null!;
        public string CriteriaId { get; set; } = null!;
        public string FormId { get; set; } = null!;
        public int? Value { get; set; }
        public string? Note { get; set; }
        public string ReportId { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? ImageUrl { get; set; }
    }
}
