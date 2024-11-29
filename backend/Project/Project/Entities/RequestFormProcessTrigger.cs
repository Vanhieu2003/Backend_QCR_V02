using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestFormProcessTrigger
    {
        public string Id { get; set; } = null!;
        public string RequestFormProcessId { get; set; } = null!;
        public int ProcessAction { get; set; }
        public string TriggerName { get; set; } = null!;
        public string ApiUrl { get; set; } = null!;
        public string HttpMethod { get; set; } = null!;
        public string? ContentType { get; set; }
        public string? Payload { get; set; }
        public string? Description { get; set; }
        public bool IsConditionTrigger { get; set; }
        public string ConditionTrigger { get; set; } = null!;

        public virtual RequestFormProcess RequestFormProcess { get; set; } = null!;
    }
}
