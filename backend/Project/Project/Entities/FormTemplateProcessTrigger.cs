using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class FormTemplateProcessTrigger
    {
        public string Id { get; set; } = null!;
        public string FormTemplateProcessId { get; set; } = null!;
        public int ProcessAction { get; set; }
        public string TriggerName { get; set; } = null!;
        public string ApiUrl { get; set; } = null!;
        public string HttpMethod { get; set; } = null!;
        public string? ContentType { get; set; }
        public string? Payload { get; set; }
        public string? Description { get; set; }
        public bool IsConditionTrigger { get; set; }
        public string ConditionTrigger { get; set; } = null!;

        public virtual FormTemplateProcess FormTemplateProcess { get; set; } = null!;
    }
}
