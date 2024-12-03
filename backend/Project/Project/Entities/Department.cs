using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Department
    {
        public Department()
        {
            FormTemplateProcessApprovers = new HashSet<FormTemplateProcessApprover>();
            FormTemplates = new HashSet<FormTemplate>();
            Lessons = new HashSet<Lesson>();
            RequestFormProcessApprovers = new HashSet<RequestFormProcessApprover>();
            Sections = new HashSet<Section>();
            UserDepartments = new HashSet<UserDepartment>();
            Specializations = new HashSet<Specialization>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Campus { get; set; }
        public string? RequestUserName { get; set; }
        public bool? IsRoom { get; set; }

        public virtual ICollection<FormTemplateProcessApprover> FormTemplateProcessApprovers { get; set; }
        public virtual ICollection<FormTemplate> FormTemplates { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<RequestFormProcessApprover> RequestFormProcessApprovers { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<UserDepartment> UserDepartments { get; set; }

        public virtual ICollection<Specialization> Specializations { get; set; }
    }
}
