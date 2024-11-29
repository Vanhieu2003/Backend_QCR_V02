using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestFormHandleWorkFlow
    {
        public RequestFormHandleWorkFlow()
        {
            RequestForms = new HashSet<RequestForm>();
        }

        public string Id { get; set; } = null!;
        public string RequestFormId { get; set; } = null!;
        public string RequestFormProcessId { get; set; } = null!;
        public int? StageStatus { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }

        public virtual RequestForm RequestForm { get; set; } = null!;
        public virtual RequestFormProcess RequestFormProcess { get; set; } = null!;
        public virtual ICollection<RequestForm> RequestForms { get; set; }
    }
}
