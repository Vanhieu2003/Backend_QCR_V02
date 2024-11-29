namespace Project.Dto
{
    public class ChartDto
    {
    }


    public class CleaningReportDetailDto
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class CleaningReportSummaryDto
    {
        public int TotalReportsToday { get; set; }
        public List<CleaningReportDetailDto> ReportCounts { get; set; }
    }

    public class CampusReportComparisonDto
    {
        public string CampusName { get; set; }
        public double AverageValue { get; set; }
        public int CountNotMet { get; set; }
        public int CountCompleted { get; set; }
        public int CountWellCompleted { get; set; }
    }

    public class ResponsibleTagReportDto
    {
        public string TagName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int TotalReport { get; set; }
        public int? Progress { get; set; }
        public string Status { get; set; }
    }

    public class RoomGroupReportDto
    {
        public string GroupName { get; set; }
        public int TotalRoom { get; set; }

        public int TotalEvaluatedRoom { get; set; }
        public int Progress { get; set; }
        public string Status { get; set; }
    }


    public class CampusDetailReportDto
    {
        public int TotalReport { get; set; }

        public int Proportion { get; set; }

        public string Status { get; set; }
    }
    public class ShiftEvaluationSummaryDto
    {
        public string ShiftName { get; set; }
        public string ShiftTime { get; set; } 
        public int TotalEvaluations { get; set; }
        public double AverageCompletionPercentage { get; set; }
        public DateTime EvaluationDate { get; set; }
    }
}
