using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestFormProcessApprover
    {
        public string Id { get; set; } = null!;
        public string RequestFormProcessId { get; set; } = null!;
        public string? DepartmentId { get; set; }
        public string? UserId { get; set; }
        public string? GroupId { get; set; }
        public bool? Approver { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Group? Group { get; set; }
        public virtual RequestFormProcess RequestFormProcess { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
