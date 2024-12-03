using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RequestFormCommunicate
    {
        public RequestFormCommunicate()
        {
            InverseRoot = new HashSet<RequestFormCommunicate>();
        }

        public string Id { get; set; } = null!;
        public string RequestFormId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? RootId { get; set; }
        public string Content { get; set; } = null!;
        public string? Attachment { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public virtual RequestForm RequestForm { get; set; } = null!;
        public virtual RequestFormCommunicate? Root { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<RequestFormCommunicate> InverseRoot { get; set; }
    }
}
