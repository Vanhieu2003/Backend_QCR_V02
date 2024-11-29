using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestForm
    {
        public RequestForm()
        {
            RequestFormActivityLogs = new HashSet<RequestFormActivityLog>();
            RequestFormCommunicates = new HashSet<RequestFormCommunicate>();
            RequestFormFields = new HashSet<RequestFormField>();
            RequestFormGroups = new HashSet<RequestFormGroup>();
            RequestFormHandleWorkFlows = new HashSet<RequestFormHandleWorkFlow>();
            RequestFormProcesses = new HashSet<RequestFormProcess>();
            RequestFormWatchers = new HashSet<RequestFormWatcher>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FormTemplateId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int FormStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string? NextWorkFlowId { get; set; }

        public virtual FormTemplate FormTemplate { get; set; } = null!;
        public virtual RequestFormHandleWorkFlow? NextWorkFlow { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<RequestFormActivityLog> RequestFormActivityLogs { get; set; }
        public virtual ICollection<RequestFormCommunicate> RequestFormCommunicates { get; set; }
        public virtual ICollection<RequestFormField> RequestFormFields { get; set; }
        public virtual ICollection<RequestFormGroup> RequestFormGroups { get; set; }
        public virtual ICollection<RequestFormHandleWorkFlow> RequestFormHandleWorkFlows { get; set; }
        public virtual ICollection<RequestFormProcess> RequestFormProcesses { get; set; }
        public virtual ICollection<RequestFormWatcher> RequestFormWatchers { get; set; }
    }
}
