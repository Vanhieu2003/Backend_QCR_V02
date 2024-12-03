using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class UserRequestBasis
    {
        public string Id { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RequestBaseId { get; set; } = null!;
        public string RequestBaseHid { get; set; } = null!;
        public string RequestBaseGid { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
