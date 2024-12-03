using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Request
    {
        public string Id { get; set; } = null!;
        public string? RequestNo { get; set; }
        public int? CategoryId { get; set; }
        public string? RequestorName { get; set; }
        public string? RequestorEmail { get; set; }
        public string? RequestorMobile { get; set; }
        public string? Description { get; set; }
        public string? RoomId { get; set; }
        public string? EquipmentId { get; set; }
        public string? EquipmentCategoryId { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public DateTime? Remind { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public int? ConfirmedBy { get; set; }
        public DateTime? StartedAt { get; set; }
        public int? StartedBy { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int? CompletedBy { get; set; }

        public virtual Equipment? Equipment { get; set; }
    }
}
