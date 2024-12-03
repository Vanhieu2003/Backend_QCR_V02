using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class UploadHistory
    {
        public string Id { get; set; } = null!;
        public string? Description { get; set; }
        public string? FileName { get; set; }
        public string? InfoNo { get; set; }
        public int? Type { get; set; }
        public bool? IsUpdated { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? FilePath { get; set; }
    }
}
