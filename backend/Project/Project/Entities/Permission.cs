using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            PermissionRoleMappings = new HashSet<PermissionRoleMapping>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<PermissionRoleMapping> PermissionRoleMappings { get; set; }
    }
}
