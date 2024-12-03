using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FormTemplateGroup
    {
        public string Id { get; set; } = null!;
        public string FormTemplateId { get; set; } = null!;
        public string GroupId { get; set; } = null!;
        public string? GroupName { get; set; }

        public virtual Group Group { get; set; } = null!;
    }
}
