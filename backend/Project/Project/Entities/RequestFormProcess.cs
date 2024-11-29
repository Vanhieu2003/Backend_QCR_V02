using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestFormProcess
    {
        public RequestFormProcess()
        {
            InverseNextProcessIfDenied = new HashSet<RequestFormProcess>();
            RequestFormHandleWorkFlows = new HashSet<RequestFormHandleWorkFlow>();
            RequestFormProcessApprovers = new HashSet<RequestFormProcessApprover>();
            RequestFormProcessTriggers = new HashSet<RequestFormProcessTrigger>();
        }

        public string Id { get; set; } = null!;
        public string RequestFormId { get; set; } = null!;
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

        public virtual RequestFormProcess? NextProcessIfDenied { get; set; }
        public virtual RequestForm RequestForm { get; set; } = null!;
        public virtual ICollection<RequestFormProcess> InverseNextProcessIfDenied { get; set; }
        public virtual ICollection<RequestFormHandleWorkFlow> RequestFormHandleWorkFlows { get; set; }
        public virtual ICollection<RequestFormProcessApprover> RequestFormProcessApprovers { get; set; }
        public virtual ICollection<RequestFormProcessTrigger> RequestFormProcessTriggers { get; set; }
    }
}
