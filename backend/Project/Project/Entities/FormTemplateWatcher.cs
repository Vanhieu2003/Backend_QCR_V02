using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FormTemplateWatcher
    {
        public string Id { get; set; } = null!;
        public string FormTemplateId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public virtual FormTemplate FormTemplate { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
