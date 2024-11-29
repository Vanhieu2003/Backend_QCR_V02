namespace Project.Dto
{
    //    public class CriteriaReportDto
    //    {
    //        public string Id { get; set; } = null!;
    //        public string CriteriaId { get; set; } = null!;
    //        public string FormId { get; set; } = null!;
    //        public int? Value { get; set; }
    //        public string? Note { get; set; }
    //        public DateTime? CreateAt { get; set; }
    //        public DateTime? UpdateAt { get; set; }
    //        public string? ReportId { get; set; }
    //    }

    //public class CriteriaReportDto
    //{
    //    public string CriteriaId { get; set; }
    //    public int Value { get; set; }
    //    public string Note { get; set; }
    //    public DateTime CreateAt { get; set; }
    //    public DateTime UpdateAt { get; set; }
    //}

    //public class ReportDto
    //{
    //    public string? Id { get; set; }
    //    public string ReportId { get; set; }
    //    public string FormId { get; set; }
    //    public List<CriteriaReportDto> CriteriaList { get; set; }
    //    public DateTime CreateAt { get; set; }
    //    public DateTime UpdateAt { get; set; }
    //}
    public class CleaningReportRequest
    {
        public string FormId { get; set; }
        public string ShiftId { get; set; }
        public string userId { get; set; }
        public List<CriteriaRequest> CriteriaList { get; set; }
    }

    public class CriteriaRequest
    {
        public string CriteriaId { get; set; }
        public int? Value { get; set; }
        public string? Note { get; set; }
        public Dictionary<string, string> Images { get; set; }
    }

}
