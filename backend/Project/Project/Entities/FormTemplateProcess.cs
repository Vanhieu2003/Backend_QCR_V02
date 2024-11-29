using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FormTemplateProcess
    {
        public FormTemplateProcess()
        {
            FormTemplateProcessApprovers = new HashSet<FormTemplateProcessApprover>();
            FormTemplateProcessTriggers = new HashSet<FormTemplateProcessTrigger>();
            InverseNextProcessIfDenied = new HashSet<FormTemplateProcess>();
        }

        public string Id { get; set; } = null!;
        public string FormTemplateId { get; set; } = null!;
        public string ProcessName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Content { get; set; }
        public int? ProcessOrder { get; set; }
        public int? ProcessStatus { get; set; }
        public int? ProcessAction { get; set; }
        public bool IsDeactivate { get; set; }
        public int? ExpirationCycle { get; set; }
        public DateTime? ExpiratedDate { get; set; }
        public int? TypeApprovalId { get; set; }
        public string? NextProcessIfDeniedId { get; set; }

        public virtual FormTemplate FormTemplate { get; set; } = null!;
        public virtual FormTemplateProcess? NextProcessIfDenied { get; set; }
        public virtual ICollection<FormTemplateProcessApprover> FormTemplateProcessApprovers { get; set; }
        public virtual ICollection<FormTemplateProcessTrigger> FormTemplateProcessTriggers { get; set; }
        public virtual ICollection<FormTemplateProcess> InverseNextProcessIfDenied { get; set; }
    }
}
