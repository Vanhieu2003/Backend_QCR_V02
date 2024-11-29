using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Criteria
    {
        public string? Id { get; set; }
        public string? CriteriaName { get; set; }
        public string? RoomCategoryId { get; set; }
        public string? CriteriaType { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? Status { get; set; }
    }
}
