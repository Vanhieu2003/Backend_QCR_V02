using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class Block
    {
        public string Id { get; set; } = null!;
        public string BlockCode { get; set; } = null!;
        public string BlockName { get; set; } = null!;
        public string? BlockName2 { get; set; }
        public string? BlockNo { get; set; }
        public string? Dvtcode { get; set; }
        public string? Dvtname { get; set; }
        public string AssetTypeCode { get; set; } = null!;
        public string AssetTypeName { get; set; } = null!;
        public int SortOrder { get; set; }
        public string? UseDepartmentCode { get; set; }
        public string? UseDepartmentName { get; set; }
        public string? ManageDepartmentCode { get; set; }
        public string? ManageDepartmentName { get; set; }
        public double FloorArea { get; set; }
        public double ContructionArea { get; set; }
        public string? FunctionCode { get; set; }
        public string? FunctionName { get; set; }
        public decimal ValueSettlement { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal CentralFunding { get; set; }
        public decimal LocalFunding { get; set; }
        public decimal OtherFunding { get; set; }
        public string StatusCode { get; set; } = null!;
        public string? StatusName { get; set; }
        public string CampusCode { get; set; } = null!;
        public string CampusName { get; set; } = null!;
        public string? CampusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
