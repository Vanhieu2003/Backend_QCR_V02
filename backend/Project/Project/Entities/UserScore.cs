using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class UserScore
    {
        public string Id { get; set; } = null!;
        public string? ReportId { get; set; }
        public string? TagId { get; set; }
        public string? UserId { get; set; }
        public int? Score { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
