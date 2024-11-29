using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class DepartmentRoleMapping
    {
        public string Id { get; set; } = null!;
        public string DepartmentId { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
