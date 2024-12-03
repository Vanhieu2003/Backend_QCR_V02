using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Room
    {
        public Room()
        {
            Lessons = new HashSet<Lesson>();
        }

        public string Id { get; set; } = null!;
        public string RoomCode { get; set; } = null!;
        public string RoomName { get; set; } = null!;
        public string? Description { get; set; }
        public string? RoomNo { get; set; }
        public string? Dvtcode { get; set; }
        public string? Dvtname { get; set; }
        public string AssetTypeCode { get; set; } = null!;
        public string AssetTypeName { get; set; } = null!;
        public string? UseDepartmentCode { get; set; }
        public string? UseDepartmentName { get; set; }
        public string? ManageDepartmentCode { get; set; }
        public string? ManageDepartmentName { get; set; }
        public double NumberOfSeats { get; set; }
        public double FloorArea { get; set; }
        public double ContructionArea { get; set; }
        public decimal ValueSettlement { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal CentralFunding { get; set; }
        public decimal LocalFunding { get; set; }
        public decimal OtherFunding { get; set; }
        public string StatusCode { get; set; } = null!;
        public string? StatusName { get; set; }
        public int SortOrder { get; set; }
        public string? BlockId { get; set; }
        public string? RoomCategoryId { get; set; }
        public string? FloorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual RoomCategory? RoomCategory { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
