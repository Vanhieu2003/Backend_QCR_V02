using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FormCollection
    {
        public FormCollection()
        {
            FormTemplates = new HashSet<FormTemplate>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<FormTemplate> FormTemplates { get; set; }
    }
}
