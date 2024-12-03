using Microsoft.EntityFrameworkCore;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class ChartRepository : IChartRepository
    {
        private readonly HcmUeQTTB_DevContext _context;
        public ChartRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }

        public async Task<List<CampusReportComparisonDto>> GetCampusReportComparison(int? year = null)
        {
            int targetYear = year ?? DateTime.Now.Year;

            var reports = await (from cr in _context.CleaningReports
                                 join cf in _context.CleaningForms on cr.FormId equals cf.Id
                                 join r in _context.Rooms on cf.RoomId equals r.Id
                                 join b in _context.Blocks on r.BlockId equals b.Id
                                 where cr.CreateAt.HasValue && cr.CreateAt.Value.Year == targetYear
                                 select new
                                 {
                                     b.CampusId,
                                     b.CampusName,
                                     cr.Value
                                 }).ToListAsync();

            var groupedReports = reports
                .GroupBy(r => new { r.CampusId, r.CampusName })
                .Select(g => new CampusReportComparisonDto
                {
                    CampusName = g.Key.CampusName,
                    AverageValue = Math.Ceiling(g.Average(r => r.Value ?? 0)),
                    CountNotMet = g.Count(value => (value.Value ?? 0) < 30),
                    CountCompleted = g.Count(value => (value.Value ?? 0) >= 30 && (value.Value ?? 0) <= 80),
                    CountWellCompleted = g.Count(value => (value.Value ?? 0) > 80)
                }).ToList();

            return groupedReports.Any() ? groupedReports : new List<CampusReportComparisonDto>();
        }




        public async Task<CleaningReportSummaryDto> GetCleaningReportSummary(string campusId)
        {
            var today = DateTime.Today;

            var query = from cr in _context.CleaningReports
                        join cf in _context.CleaningForms on cr.FormId equals cf.Id
                        join r in _context.Rooms on cf.RoomId equals r.Id
                        join b in _context.Blocks on r.BlockId equals b.Id
                        where cr.CreateAt.HasValue && cr.CreateAt.Value.Date == today
                        select new { cr, b.CampusId };

            if (!string.IsNullOrEmpty(campusId))
            {
                query = query.Where(q => q.CampusId == campusId);
            }

            var reports = await query.Select(q => q.cr.Value).ToListAsync();

            int totalReportsToday = reports.Count;
            int countNotMet = reports.Count(value => value < 30);
            int countCompleted = reports.Count(value => value >= 30 && value < 80);
            int countWellCompleted = reports.Count(value => value >= 80);

            var reportCounts = new List<CleaningReportDetailDto>
    {
        new CleaningReportDetailDto { Status = "Chưa hoàn thành", Count = countNotMet },
        new CleaningReportDetailDto { Status = "Hoàn thành", Count = countCompleted },
        new CleaningReportDetailDto { Status = "Hoàn thành tốt", Count = countWellCompleted }
    };

            return new CleaningReportSummaryDto
            {
                TotalReportsToday = totalReportsToday,
                ReportCounts = reportCounts
            };
        }




        public async Task<List<ResponsibleTagReportDto>> GetResponsibleTagReportByCampus(string? campusId)
        {
            var today = DateTime.Today;

            var query = from us in _context.UserScores
                        join user in _context.Users on us.UserId equals user.Id
                        join tag in _context.Tags on us.TagId equals tag.Id
                        join cleaningreport in _context.CleaningReports on us.ReportId equals cleaningreport.Id
                        join cleaningform in _context.CleaningForms on cleaningreport.FormId equals cleaningform.Id
                        join rooms in _context.Rooms on cleaningform.RoomId equals rooms.Id
                        join block in _context.Blocks on rooms.BlockId equals block.Id
                        where (campusId == null || block.CampusId == campusId)
                              && us.CreateAt.HasValue
                              && us.CreateAt.Value.Date == today
                        group new { us, cleaningreport } by new { tag.TagName, user.LastName, user.FirstName } into tagGroup
                        select new ResponsibleTagReportDto
                        {
                            TagName = tagGroup.Key.TagName,
                            LastName = tagGroup.Key.LastName,
                            FirstName = tagGroup.Key.FirstName,
                            TotalReport = tagGroup.Count(),
                            Progress = (int)Math.Ceiling(tagGroup.Average(x => x.us.Score ?? 0)),
                            Status = tagGroup.Average(x => x.us.Score ?? 0) >= 80 ? "Hoàn thành tốt" : "Cần cải thiện"
                        };

            var result = await query.ToListAsync();
            return result.Any() ? result : new List<ResponsibleTagReportDto>();
        }


        public async Task<List<RoomGroupReportDto>> GetRoomGroupReportByCampus(string? campusId)
        {
            var today = DateTime.Today;

            var query = from roomgroup in _context.RoomByGroups
                        join rooms in _context.Rooms on roomgroup.RoomId equals rooms.Id
                        join grouprooms in _context.GroupRooms on roomgroup.GroupRoomId equals grouprooms.Id
                        join block in _context.Blocks on rooms.BlockId equals block.Id
                        where campusId == null || block.CampusId == campusId
                        group new { rooms, roomgroup } by new { grouprooms.GroupName } into Group
                        select new
                        {
                            GroupName = Group.Key.GroupName,
                            TotalRoom = Group.Select(g => g.rooms.Id).Distinct().Count()
                        };

            var evaluatedQuery = from roomgroup in _context.RoomByGroups
                                 join rooms in _context.Rooms on roomgroup.RoomId equals rooms.Id
                                 join grouprooms in _context.GroupRooms on roomgroup.GroupRoomId equals grouprooms.Id
                                 join cleaningform in _context.CleaningForms on rooms.Id equals cleaningform.RoomId
                                 join cleaningreport in _context.CleaningReports on cleaningform.Id equals cleaningreport.FormId
                                 join block in _context.Blocks on rooms.BlockId equals block.Id
                                 where (campusId == null || block.CampusId == campusId) &&
                                       cleaningreport.CreateAt.HasValue &&
                                       cleaningreport.CreateAt.Value.Date == today
                                 group cleaningreport by new { grouprooms.GroupName } into Group
                                 select new
                                 {
                                     GroupName = Group.Key.GroupName,
                                     TotalEvaluatedRoom = Group.Count(),
                                     AverageScore = Group.Average(g => g.Value ?? 0)
                                 };

            var roomGroups = await query.ToListAsync();
            var evaluatedRooms = await evaluatedQuery.ToListAsync();

            var result = (from rg in roomGroups
                          join er in evaluatedRooms on rg.GroupName equals er.GroupName into evaluatedGroup
                          from eg in evaluatedGroup.DefaultIfEmpty()
                          select new RoomGroupReportDto
                          {
                              GroupName = rg.GroupName,
                              TotalRoom = rg.TotalRoom,
                              TotalEvaluatedRoom = eg?.TotalEvaluatedRoom ?? 0,
                              Progress = eg != null ? (int)Math.Ceiling(eg.AverageScore) : 0,
                              Status = eg != null
                                  ? eg.AverageScore > 80 ? "Hoàn thành tốt"
                                    : eg.AverageScore < 30 ? "Chưa hoàn thành"
                                    : "Cần cải thiện"
                                  : "Chưa hoàn thành"
                          }).ToList();

            return result.Any() ? result : new List<RoomGroupReportDto>();
        }




        public async Task<List<CampusDetailReportDto>> GetCampusDetailReportById(string? campusId)
        {
            var today = DateTime.Today;

            var query = from rooms in _context.Rooms
                        join cleaningform in _context.CleaningForms on rooms.Id equals cleaningform.RoomId
                        join cleaningreport in _context.CleaningReports on cleaningform.Id equals cleaningreport.FormId
                        join block in _context.Blocks on rooms.BlockId equals block.Id
                        where (campusId == null || block.CampusId == campusId)
                              && cleaningreport.CreateAt.HasValue
                              && cleaningreport.CreateAt.Value.Date == today
                        group cleaningreport by rooms.Id into roomGroup
                        select new
                        {
                            RoomId = roomGroup.Key,
                            AverageScore = roomGroup.Average(r => r.Value ?? 0)
                        };

            var result = await query.ToListAsync();

            // Tính toán tổng số lượng phòng và các mức độ hoàn thành
            int totalRooms = result.Count;
            int wellCompletedRooms = result.Count(r => r.AverageScore > 80);
            int notMetRooms = result.Count(r => r.AverageScore < 30);
            int completedRooms = totalRooms - wellCompletedRooms - notMetRooms;

            // Tính tỷ lệ phần trăm cho từng mức độ
            double wellCompletedPercentage = totalRooms > 0 ? (double)wellCompletedRooms / totalRooms * 100 : 0;
            double notMetPercentage = totalRooms > 0 ? (double)notMetRooms / totalRooms * 100 : 0;
            double completedPercentage = totalRooms > 0 ? (double)completedRooms / totalRooms * 100 : 0;

            // Tạo danh sách kết quả
            var campusDetailReports = new List<CampusDetailReportDto>
    {
        new CampusDetailReportDto
        {
            TotalReport = wellCompletedRooms,
            Proportion = (int)Math.Round(wellCompletedPercentage),
            Status = "Hoàn thành tốt"
        },
        new CampusDetailReportDto
        {
            TotalReport = notMetRooms,
            Proportion = (int)Math.Round(notMetPercentage),
            Status = "Chưa hoàn thành"
        },
        new CampusDetailReportDto
        {
            TotalReport = completedRooms,
            Proportion = (int)Math.Round(completedPercentage),
            Status = "Hoàn thành"
        }
    };

            return campusDetailReports;
        }


        public async Task<IEnumerable<ShiftEvaluationSummaryDto>> GetShiftEvaluationsAsync(string? campusId = null)
        {
            var today = DateTime.Today;

            var query = from cleaningreport in _context.CleaningReports
                        join cleaningform in _context.CleaningForms on cleaningreport.FormId equals cleaningform.Id
                        join rooms in _context.Rooms on cleaningform.RoomId equals rooms.Id
                        join block in _context.Blocks on rooms.BlockId equals block.Id
                        join shifts in _context.Shifts on cleaningreport.ShiftId equals shifts.Id
                        where cleaningreport.CreateAt.HasValue
                              && cleaningreport.CreateAt.Value.Date == today
                              && (string.IsNullOrEmpty(campusId) || block.CampusId == campusId)
                        group cleaningreport by new
                        {
                            shifts.ShiftName,
                            shifts.StartTime,
                            shifts.EndTime,
                            EvaluationDate = cleaningreport.CreateAt.Value.Date
                        } into Group
                        select new ShiftEvaluationSummaryDto
                        {
                            ShiftName = Group.Key.ShiftName,
                            ShiftTime = $"{Group.Key.StartTime:hh\\:mm} - {Group.Key.EndTime:hh\\:mm}",
                            TotalEvaluations = Group.Count(),
                            AverageCompletionPercentage = Group.Any(g => g.Value != null)
                                ? (int)Math.Round(Group.Average(g => g.Value ?? 0))
                                : 0,
                            EvaluationDate = Group.Key.EvaluationDate
                        };

            return await query.OrderBy(dto => dto.EvaluationDate)
                               .ThenBy(dto => dto.ShiftName)
                               .ToListAsync();
        }

        public async Task<List<CriteriaValueDto>> GetTopCriteriaValuesByCampus(string? campusId)
        {
            var query = from cf in _context.CleaningForms
                        join crr in _context.CriteriaReports on cf.Id equals crr.FormId
                        join cr in _context.Criteria on crr.CriteriaId equals cr.Id
                        join r in _context.Rooms on cf.RoomId equals r.Id
                        join b in _context.Blocks on r.BlockId equals b.Id
                        join ca in _context.Campuses on b.CampusId equals ca.Id
                        where string.IsNullOrEmpty(campusId) || ca.Id == campusId
                        group new { crr, cr, ca } by new { ca.CampusName, cr.CriteriaName, cr.CriteriaType } into grouped
                        select new CriteriaValueDto
                        {
                            CampusName = grouped.Key.CampusName,
                            CriteriaName = grouped.Key.CriteriaName,
                            Value = grouped.Key.CriteriaType == "RATING"
                                    ? (int)Math.Ceiling((double)(grouped.Sum(x => x.crr.Value) / (grouped.Count() * 5.0)) * 100)
                                    : grouped.Key.CriteriaType == "BINARY"
                                    ? (int)Math.Ceiling((double)(grouped.Sum(x => x.crr.Value) / (grouped.Count() * 2.0)) * 100)
                                    : 0
                        };

            var result = await query.OrderByDescending(x => x.Value)
                                     .Take(5)
                                     .ToListAsync();

            return result;
        }
        public async Task<CleaningReportSummaryDto> GetReportInADayAsync()
        {
            var today = DateTime.Today;

            var totalRooms = await _context.CleaningForms.CountAsync();

            var totalReportsToday = await _context.CleaningReports
                .Where(cr => EF.Functions.DateDiffDay(cr.UpdateAt, today) == 0)
                .Select(cr => cr.FormId)
                .Distinct()
                .CountAsync();

            var result = new CleaningReportSummaryDto
            {
                TotalReportsToday = totalReportsToday,
                ReportCounts = new List<CleaningReportDetailDto>
            {
                new CleaningReportDetailDto
                {
                    Status = "Total Rooms",
                    Count = totalRooms
                }
            }
            };

            return result;
        }
       


        public async Task<List<CleaningReportDto>> GetCleaningReportsByQuarter()
        {
            var endDate = DateTime.Now;
            var quarters = Enumerable.Range(0, 4)
                .Select(i => new
                {
                    QuarterNum = (endDate.AddMonths(-3 * i).Month - 1) / 3 + 1,
                    YearNum = endDate.AddMonths(-3 * i).Year
                })
                .Select(q => (q.QuarterNum, q.YearNum))
                .ToList();

            return await GetCampusDataByQuarter(quarters);
        }




        public async Task<List<CleaningReportDto>> GetCleaningReportsByMonth(int? month = null, int? year = null)
        {
            int currentMonth = month ?? DateTime.Now.Month;
            int currentYear = year ?? DateTime.Now.Year;

            if (year > DateTime.Now.Year)
            {
                return new List<CleaningReportDto>();
            }

            var daysInMonth = Enumerable.Range(1, DateTime.DaysInMonth(currentYear, currentMonth))
                .Select(day => new DateTime(currentYear, currentMonth, day))
                .ToList();

            
            var hasData = await _context.CleaningReports
                .AnyAsync(cr => cr.UpdateAt.HasValue && cr.UpdateAt.Value.Year == currentYear && cr.UpdateAt.Value.Month == currentMonth);

            if (!hasData)
            {
                return new List<CleaningReportDto>();  
            }

            return await GetCampusDataByDates(daysInMonth, currentMonth, currentYear);
        }


        private async Task<List<CleaningReportDto>> GetCampusDataByDates(List<DateTime> dates, int? month = null, int? year = null)
        {
            
            if (year > DateTime.Now.Year)
            {
                return new List<CleaningReportDto>(); 
            }

            
            month ??= DateTime.Now.Month;
            year ??= DateTime.Now.Year;

            var campuses = await _context.Campuses.ToListAsync();
            var blocks = await _context.Blocks.ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();
            var cleaningForms = await _context.CleaningForms.ToListAsync();
            var cleaningReports = await _context.CleaningReports.ToListAsync();

            var result = dates
                .Select(day => new
                {
                    ReportDate = day,
                    CampusData = campuses.Select(c => new
                    {
                        CampusName = c.CampusName,
                        AverageValue = (
                            from cr in cleaningReports
                            join cf in cleaningForms on cr.FormId equals cf.Id
                            join r in rooms on cf.RoomId equals r.Id
                            join b in blocks on r.BlockId equals b.Id
                            where b.CampusId == c.Id &&
                                  cr.UpdateAt.HasValue &&
                                  cr.UpdateAt.Value.Date == day
                            select cr.Value
                        ).Average(crValue => (double?)crValue) ?? 0
                    })
                })
                .SelectMany(x => x.CampusData.Select(cd => new CleaningReportDto
                {
                    CampusName = cd.CampusName,
                    ReportTime = x.ReportDate.ToString("dd-MM-yyyy"),
                    AverageValue = (int)cd.AverageValue
                }))
                .OrderBy(x => x.ReportTime)
                .ToList();

            return result;
        }


        private async Task<List<CleaningReportDto>> GetCampusDataByQuarter(List<(int QuarterNum, int YearNum)> quarters)
        {
            var campuses = await _context.Campuses.ToListAsync();
            var blocks = await _context.Blocks.ToListAsync();
            var rooms = await _context.Rooms.ToListAsync();
            var cleaningForms = await _context.CleaningForms.ToListAsync();
            var cleaningReports = await _context.CleaningReports.ToListAsync();

            var result = quarters
                .Select(q => new
                {
                    QuarterNum = q.QuarterNum,
                    YearNum = q.YearNum,
                    CampusData = campuses.Select(c => new
                    {
                        CampusName = c.CampusName,
                        AverageValue = (
                            from cr in cleaningReports
                            join cf in cleaningForms on cr.FormId equals cf.Id
                            join r in rooms on cf.RoomId equals r.Id
                            join b in blocks on r.BlockId equals b.Id
                            where b.CampusId == c.Id &&
                                  cr.UpdateAt.HasValue &&
                                  cr.UpdateAt.Value.Year == q.YearNum &&
                                  (cr.UpdateAt.Value.Month - 1) / 3 + 1 == q.QuarterNum
                            select cr.Value
                        ).Average(crValue => (double?)crValue) ?? 0
                    })
                })
                .SelectMany(x => x.CampusData.Select(cd => new CleaningReportDto
                {
                    CampusName = cd.CampusName,
                    ReportTime = $"Q{x.QuarterNum}-{x.YearNum}",
                    AverageValue = (int)cd.AverageValue
                }))
                .OrderBy(x => x.ReportTime)
                .ToList();

            return result;
        }


    }
}
