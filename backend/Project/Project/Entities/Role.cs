using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Role
    {
        public Role()
        {
            DepartmentRoleMappings = new HashSet<DepartmentRoleMapping>();
            PermissionRoleMappings = new HashSet<PermissionRoleMapping>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<DepartmentRoleMapping> DepartmentRoleMappings { get; set; }
        public virtual ICollection<PermissionRoleMapping> PermissionRoleMappings { get; set; }
    }
}
