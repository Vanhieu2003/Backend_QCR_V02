using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class EquipmentCategory
    {
        public EquipmentCategory()
        {
            Equipment = new HashSet<Equipment>();
        }

        public string Id { get; set; } = null!;
        public string? CategoryName { get; set; }
        public string? CategoryCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }
    }
}
