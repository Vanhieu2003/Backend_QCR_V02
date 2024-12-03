using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class PermissionRoleMapping
    {
        public string Id { get; set; } = null!;
        public string PermissionId { get; set; } = null!;
        public string RoleId { get; set; } = null!;

        public virtual Permission Permission { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}
