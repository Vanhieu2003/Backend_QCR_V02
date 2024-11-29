using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FormTemplate
    {
        public FormTemplate()
        {
            FieldTemplates = new HashSet<FieldTemplate>();
            FormTemplateFields = new HashSet<FormTemplateField>();
            FormTemplateProcesses = new HashSet<FormTemplateProcess>();
            FormTemplateWatchers = new HashSet<FormTemplateWatcher>();
            RequestForms = new HashSet<RequestForm>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? FormCollectionId { get; set; }
        public string? DepartmentId { get; set; }
        public int? ExpirationCycle { get; set; }
        public string? Description { get; set; }
        public bool Deactivate { get; set; }
        public int FormStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime? ExpiratedDate { get; set; }
        public string? CreatedById { get; set; }

        public virtual Department? Department { get; set; }
        public virtual FormCollection? FormCollection { get; set; }
        public virtual ICollection<FieldTemplate> FieldTemplates { get; set; }
        public virtual ICollection<FormTemplateField> FormTemplateFields { get; set; }
        public virtual ICollection<FormTemplateProcess> FormTemplateProcesses { get; set; }
        public virtual ICollection<FormTemplateWatcher> FormTemplateWatchers { get; set; }
        public virtual ICollection<RequestForm> RequestForms { get; set; }
    }
}
