using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class TagsPerCriteria
    {
        public string? Id { get; set; }
        public string? CriteriaId { get; set; }
        public string? TagId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
