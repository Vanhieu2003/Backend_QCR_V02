using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class CriteriasPerForm
    {
        public string Id { get; set; } = null!;
        public string CriteriaId { get; set; } = null!;
        public string FormId { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
