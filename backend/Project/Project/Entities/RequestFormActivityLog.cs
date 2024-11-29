using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestFormActivityLog
    {
        public string Id { get; set; } = null!;
        public string RequestFormId { get; set; } = null!;
        public int? FormStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Note { get; set; }
        public string UserId { get; set; } = null!;

        public virtual RequestForm RequestForm { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
