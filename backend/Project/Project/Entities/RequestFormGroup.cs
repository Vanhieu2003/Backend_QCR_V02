using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestFormGroup
    {
        public string Id { get; set; } = null!;
        public string RequestFormId { get; set; } = null!;
        public string GroupId { get; set; } = null!;
        public string? GroupName { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual RequestForm RequestForm { get; set; } = null!;
    }
}
