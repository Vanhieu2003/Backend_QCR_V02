using System;
using System.Collections.Generic;
using System.Data;

namespace Project.Entities
{
    public partial class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
