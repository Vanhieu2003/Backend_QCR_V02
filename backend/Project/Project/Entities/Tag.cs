using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Tag
    {
        public string Id { get; set; } = null!;
        public string TagName { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
