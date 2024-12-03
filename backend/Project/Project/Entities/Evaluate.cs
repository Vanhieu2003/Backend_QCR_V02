using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Evaluate
    {
        public string Id { get; set; } = null!;
        public string EvaluateName { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
