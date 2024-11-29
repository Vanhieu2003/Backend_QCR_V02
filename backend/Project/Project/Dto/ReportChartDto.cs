namespace Project.Dto
{
    public class CampusAverageValueDto
    {
        public string CampusName { get; set; }
        public int? Day { get; set; }
        public int? AverageValue { get; set; }
    }

    public class CriteriaValueDto
    {
        public string? CampusName { get; set; }
        public string? CriteriaName { get; set; }
        public decimal Value { get; set; }
    }
    public class ReportInADayValueDto
    {
        public int? totalRooms { get; set; }
        public int? totalReports { get; set; }
    }

    public class CleaningReportDto
    {
        public string CampusName { get; set; }
        public string ReportTime { get; set; } // Qn-yyyy format
        public int? AverageValue { get; set; } // Average value
    }

    public class CleaningReportYearDto
    {
        public string CampusName { get; set; }
        public int ReportTime { get; set; } // Qn-yyyy format
        public int? AverageValue { get; set; } // Average value
    }

    public class BlockReportDto
    {
        public string BlockName { get; set; }
        public int TotalRooms { get; set; }
        public int TotalEvaluatedRooms { get; set; }
        public int AverageCompletionValue { get; set; }
        public int CompletionPercentage { get; set; }
    }

    public class UserScoreDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public double AverageScore { get; set; }
    }



}
