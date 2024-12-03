namespace Project.Dto
{
    //public class CleaningReportDto
    //{
    //    public string Id { get; set; } = null!;
    //    public string? FormId { get; set; }
    //    public int? Value { get; set; } // Trung bình từ CriteriaReport
    //    public string UserId { get; set; } = null!;
    //    public string ShiftId { get; set; } = null!;
    //    public DateTime? CreateAt { get; set; }

    //}
    public class CleaningReportCountDto
    {
        public int TotalCount { get; set; }
        public int TotalCountYesterday { get; set; }
        public int TotalReports { get; set; }
        public int Change { get; set; }
    }
    public class CleaningReportDetailsDto
    {
        public string id { get; set; } = null!;
        public string? formId { get; set; }
        public string? campusName { get; set; }
        public string? blockName { get; set; }
        public string? floorName { get; set; }
        public string? roomName { get; set; }
        public int? value { get; set; }
        public string userId { get; set; } = null!;
        public string? shiftName { get; set; }
        public string? startTime { get; set; }
        public string? endTime { get; set; }
        public DateTime? createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
    public class UpdateCleaningReportRequest
    {
        public string ReportId { get; set; }
        public List<UpdateCriteriaDto> CriteriaList { get; set; }
        public List<UserPerTagRequest> UserPerTags { get; set; }
    }

    public class UpdateCriteriaDto
    {
        public string Id { get; set; }
        public int? Value { get; set; }
        public string? Note { get; set; }
        public Dictionary<string,string>? Images { get; set; }
    }
}
