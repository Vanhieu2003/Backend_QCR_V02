using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FieldTemplate
    {
        public FieldTemplate()
        {
            FormTemplateFields = new HashSet<FormTemplateField>();
        }

        public string Id { get; set; } = null!;
        public string FieldName { get; set; } = null!;
        public int TypeInput { get; set; }
        public string? Description { get; set; }
        public string? InputKey { get; set; }
        public string? InputMask { get; set; }
        public string? InitialData { get; set; }
        public string? DefaultDataSource { get; set; }
        public bool? IsRequired { get; set; }
        public bool? IsHiddenField { get; set; }
        public bool? IsDeactivate { get; set; }
        public int? DisplayOrder { get; set; }
        public string FormTemplateId { get; set; } = null!;

        public virtual FormTemplate FormTemplate { get; set; } = null!;
        public virtual ICollection<FormTemplateField> FormTemplateFields { get; set; }
    }
}
