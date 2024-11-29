using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class EquipmentCategoryGroup
    {
        public string Id { get; set; } = null!;
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
    }
}
