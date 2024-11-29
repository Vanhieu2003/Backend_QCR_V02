using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class UserDepartment
    {
        public string Id { get; set; } = null!;
        public string? UserId { get; set; }
        public string? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual User? User { get; set; }
    }
}
