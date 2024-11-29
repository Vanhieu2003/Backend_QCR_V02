using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Group
    {
        public Group()
        {
            FormTemplateGroups = new HashSet<FormTemplateGroup>();
            FormTemplateProcessApprovers = new HashSet<FormTemplateProcessApprover>();
            RequestFormGroups = new HashSet<RequestFormGroup>();
            RequestFormProcessApprovers = new HashSet<RequestFormProcessApprover>();
            UserGroups = new HashSet<UserGroup>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<FormTemplateGroup> FormTemplateGroups { get; set; }
        public virtual ICollection<FormTemplateProcessApprover> FormTemplateProcessApprovers { get; set; }
        public virtual ICollection<RequestFormGroup> RequestFormGroups { get; set; }
        public virtual ICollection<RequestFormProcessApprover> RequestFormProcessApprovers { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
