using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class ResponsiblePerson
    {
        public string Id { get; set; } = null!;
        public string? PersonName { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
