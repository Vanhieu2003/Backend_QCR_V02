using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Equipment
    {
        public Equipment()
        {
            Requests = new HashSet<Request>();
        }

        public string Id { get; set; } = null!;
        public string EquipmentCode { get; set; } = null!;
        public string EquipmentName { get; set; } = null!;
        public string? EquipmentName2 { get; set; }
        public string? Dvtcode { get; set; }
        public string? Dvtname { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public string? BarCode { get; set; }
        public string? Specification { get; set; }
        public DateTime? ManufactDate { get; set; }
        public string? VehicleType { get; set; }
        public string? LicensePlates { get; set; }
        public int NumberOfSeats { get; set; }
        public string? ProductCountry { get; set; }
        public int ManufactYear { get; set; }
        public string? DriverName { get; set; }
        public string? FrequencyUse { get; set; }
        public string? UseDepartmentCode { get; set; }
        public string? UseDepartmentName { get; set; }
        public string? ManageDepartmentCode { get; set; }
        public string? ManageDepartmentName { get; set; }
        public double Quantily { get; set; }
        public decimal ValueSettlement { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal CentralFunding { get; set; }
        public decimal LocalFunding { get; set; }
        public decimal OtherFunding { get; set; }
        public string StatusCode { get; set; } = null!;
        public string? StatusName { get; set; }
        public string? SuperiorsAssetCode { get; set; }
        public string AssetTypeGroupCode { get; set; } = null!;
        public string AssetTypeGroupName { get; set; } = null!;
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? AssetGroupCode { get; set; }
        public string AssetGroupName { get; set; } = null!;
        public int SortOrder { get; set; }
        public string FatypeId { get; set; } = null!;
        public string? EquipmentCategoryId { get; set; }
        public string? RoomId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual EquipmentCategory? EquipmentCategory { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
