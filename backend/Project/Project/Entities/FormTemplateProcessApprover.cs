using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FormTemplateProcessApprover
    {
        public string Id { get; set; } = null!;
        public string FormTemplateProcessId { get; set; } = null!;
        public string? DepartmentId { get; set; }
        public string? UserId { get; set; }
        public string? GroupId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual FormTemplateProcess FormTemplateProcess { get; set; } = null!;
        public virtual Group? Group { get; set; }
        public virtual User? User { get; set; }
    }
}
