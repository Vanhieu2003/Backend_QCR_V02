using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using Project.Dto;
using Project.Entities;
using Project.Interface;

namespace Project.Repository
{
    public class CleaningReportRepository : ICleaningReportRepository
    {
        public readonly HcmUeQTTB_DevContext _context;
        public CleaningReportRepository(HcmUeQTTB_DevContext context)
        {
            _context = context;
        }
        public async Task<List<CleaningReport>> GetReportsByShiftId(string shiftId)
        {
            return await _context.CleaningReports
                .Where(cr => cr.ShiftId == shiftId)
                .ToListAsync();
        }
        public async Task<List<CleaningReport>> GetCleaningReportByCleaningForm(string formId)
        {
            var cleaningreport = await _context.CleaningReports.Where(c => c.FormId == formId).ToListAsync();
            return cleaningreport;
        }

        public async Task<CleaningReportDetailsDto> GetInfoByReportId(string reportId)
        {
            var reportDetails = await (from cr in _context.CleaningReports
                                       join cf in _context.CleaningForms on cr.FormId equals cf.Id
                                       join r in _context.Rooms on cf.RoomId equals r.Id
                                       join b in _context.Blocks on r.BlockId equals b.Id
                                       join c in _context.Campuses on b.CampusId equals c.Id
                                       join f in _context.Floors on r.FloorId equals f.Id
                                       where cr.Id == reportId
                                       select new CleaningReportDetailsDto
                                       {
                                           id = cr.Id,
                                           formId = cr.FormId,
                                           campusName = c.CampusName,
                                           blockName = b.BlockName,
                                           floorName = f.FloorName,
                                           roomName = r.RoomName,
                                           value = cr.Value,
                                           userId = cr.UserId,
                                           createAt = cr.CreateAt,
                                           updateAt = cr.UpdateAt
                                       }).FirstOrDefaultAsync();

            return reportDetails;
        }

        public async Task<List<CleaningReportDetailsDto>> GetReportInfo()
        {
            // Tạo truy vấn lấy thông tin báo cáo và tổng số lượng báo cáo
            var reportDetailsQuery = from cr in _context.CleaningReports
                                     join s in _context.Shifts on cr.ShiftId equals s.Id
                                     join cf in _context.CleaningForms on cr.FormId equals cf.Id
                                     join r in _context.Rooms on cf.RoomId equals r.Id
                                     join b in _context.Blocks on r.BlockId equals b.Id
                                     join c in _context.Campuses on b.CampusId equals c.Id
                                     join f in _context.Floors on r.FloorId equals f.Id
                                     select new CleaningReportDetailsDto
                                     {
                                         id = cr.Id,
                                         formId = cr.FormId,
                                         campusName = c.CampusName,
                                         blockName = b.BlockName,
                                         floorName = f.FloorName,
                                         roomName = r.RoomName,
                                         value = cr.Value,
                                         userId = cr.UserId,
                                         startTime = s.StartTime.ToString("hh\\:mm"),
                                         endTime = s.EndTime.ToString("hh\\:mm"),
                                         createAt = cr.CreateAt,
                                         updateAt = cr.UpdateAt
                                     };

            // Sắp xếp báo cáo theo thời gian tạo mới nhất
            reportDetailsQuery = reportDetailsQuery.OrderByDescending(r => r.createAt);

          
            var reports = await reportDetailsQuery.ToListAsync();

            return reports;
        }

        public async Task<List<CleaningReportDetailsDto>> GetReportInfoByUserId(string userId)
        {
            
            var reportDetailsQuery = from cr in _context.CleaningReports
                                     join s in _context.Shifts on cr.ShiftId equals s.Id
                                     join cf in _context.CleaningForms on cr.FormId equals cf.Id
                                     join r in _context.Rooms on cf.RoomId equals r.Id
                                     join b in _context.Blocks on r.BlockId equals b.Id
                                     join c in _context.Campuses on b.CampusId equals c.Id
                                     join f in _context.Floors on r.FloorId equals f.Id
                                     join us in _context.UserScores on cr.Id equals us.ReportId
                                     where us.UserId == userId
                                     select new CleaningReportDetailsDto
                                     {
                                         id = cr.Id,
                                         formId = cr.FormId,
                                         campusName = c.CampusName,
                                         blockName = b.BlockName,
                                         floorName = f.FloorName,
                                         roomName = r.RoomName,
                                         value = cr.Value,
                                         userId = cr.UserId,
                                         startTime = s.StartTime.ToString("hh\\:mm"),
                                         endTime = s.EndTime.ToString("hh\\:mm"),
                                         createAt = cr.CreateAt,
                                         updateAt = cr.UpdateAt
                                     };

            // Sắp xếp báo cáo theo thời gian tạo mới nhất
            reportDetailsQuery = reportDetailsQuery.OrderByDescending(r => r.createAt);


            var reports = await reportDetailsQuery.ToListAsync();

            return reports;
        }

        public async Task<CleaningReport> UpdateCriteriaAndCleaningReport(UpdateCleaningReportRequest request)
        {
            // 1. Kiểm tra xem CleaningReport có tồn tại không dựa trên ReportId
            var cleaningReport = await _context.CleaningReports
                .FirstOrDefaultAsync(cr => cr.Id == request.ReportId);

            if (cleaningReport == null)
            {
                throw new KeyNotFoundException("CleaningReport không tồn tại.");
            }

            // 2. Cập nhật CriteriaReports theo CriteriaList
            foreach (var criteriaDto in request.CriteriaList)
            {
                var criteriaReport = await _context.CriteriaReports
                    .FirstOrDefaultAsync(cr => cr.CriteriaId == criteriaDto.Id && cr.ReportId == request.ReportId);

                if (criteriaReport == null)
                {
                    throw new KeyNotFoundException($"CriteriaReport với CriteriaId '{criteriaDto.Id}' không tồn tại cho ReportId '{request.ReportId}'.");
                }

                criteriaReport.Value = criteriaDto.Value;
                criteriaReport.ReportId = request.ReportId;
                criteriaReport.Note = criteriaDto.Note;
                criteriaReport.UpdateAt = GetCurrentTimeInGmtPlus7();
                criteriaReport.ImageUrl = JsonConvert.SerializeObject(criteriaDto.Images);
                _context.CriteriaReports.Update(criteriaReport);
            }

            await _context.SaveChangesAsync();

            // 3. Tính toán giá trị tổng hợp và cập nhật CleaningReport
            var criteriaReports = await _context.CriteriaReports
                .Where(cr => cr.ReportId == request.ReportId)
                .ToListAsync();

            var criteriaList = await _context.Criteria
                .Where(c => criteriaReports.Select(cr => cr.CriteriaId).Contains(c.Id))
                .ToListAsync();

            var totalMaxValue = criteriaList.Sum(c => c.CriteriaType == "BINARY" ? 1 : 5);
            var totalValue = criteriaReports.Sum(cr => cr.Value ?? 0);
            var percentage = totalMaxValue > 0 ? (double)totalValue / totalMaxValue * 100 : 0;
            var roundedPercentage = Math.Round(percentage, 2);

            cleaningReport.Value = (int)roundedPercentage;
            cleaningReport.UpdateAt = GetCurrentTimeInGmtPlus7();
            _context.CleaningReports.Update(cleaningReport);

            // 4. Cập nhật điểm cho từng người dùng
            foreach (var userPerTag in request.UserPerTags)
            {
                var tagId = userPerTag.TagId;

                var criteriaIdsForTag = await _context.TagsPerCriteria
                    .Where(tp => tp.TagId == tagId)
                    .Select(tp => tp.CriteriaId)
                    .ToListAsync();

                var relatedCriteriaList = request.CriteriaList
                    .Where(c => criteriaIdsForTag.Contains(c.Id))
                    .ToList();

                if (!relatedCriteriaList.Any()) continue;

                double totalScore = 0;
                int totalCriteriaTypeScore = 0;

                foreach (var criteria in relatedCriteriaList)
                {
                    var criteriaEntity = await _context.Criteria.FindAsync(criteria.Id);
                    var multiplier = criteriaEntity.CriteriaType == "BINARY" ? 1 : 5;
                    totalCriteriaTypeScore += multiplier;
                    totalScore += (double)criteria.Value;
                }

                var finalScore = Math.Round((double)(totalScore / totalCriteriaTypeScore), 2) * 100;

                foreach (var user in userPerTag.Users)
                {
                    var existingUserScore = await _context.UserScores
                        .FirstOrDefaultAsync(us => us.ReportId == request.ReportId && us.TagId == tagId && us.UserId == user.Id);

                    if (existingUserScore != null)
                    {
                        existingUserScore.Score = (int)finalScore;
                        existingUserScore.UpdateAt = GetCurrentTimeInGmtPlus7();
                        _context.UserScores.Update(existingUserScore);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return cleaningReport;
        }

        public async Task<List<UserScore>> EvaluateUserScores(EvaluationRequest request)
        {
            var userArray = new List<UserScore>();

            foreach (var userPerTag in request.UserPerTags)
            {
                var tagId = userPerTag.TagId;

                
                var criteriaIdsForTag = await _context.TagsPerCriteria
                    .Where(tp => tp.TagId == tagId)
                    .Select(tp => tp.CriteriaId)
                    .ToListAsync();

              
                var relatedCriteriaList = request.CriteriaList
                    .Where(c => criteriaIdsForTag.Contains(c.CriteriaId))
                    .ToList();

               
                if (!relatedCriteriaList.Any()) continue;

               
                double totalScore = 0;
                int totalCriteriaTypeScore = 0;

                foreach (var criteria in relatedCriteriaList)
                {
                    var criteriaEntity = await _context.Criteria.FindAsync(criteria.CriteriaId);
                    var multiplier = criteriaEntity.CriteriaType == "BINARY" ? 1 : 5;
                    totalCriteriaTypeScore += multiplier;
                    totalScore += (double)criteria.Value;
                }

                
                var finalScore = Math.Round((double)(totalScore / totalCriteriaTypeScore), 2) * 100;

                foreach (var user in userPerTag.Users)
                {
                    var userScore = new UserScore
                    {
                        Id = Guid.NewGuid().ToString(),
                        ReportId = request.ReportId,
                        TagId = tagId,
                        UserId = user.Id,
                        Score = (int)finalScore,
                        CreateAt = GetCurrentTimeInGmtPlus7(),
                        UpdateAt = GetCurrentTimeInGmtPlus7()
                    };

                    _context.UserScores.Add(userScore);
                    userArray.Add(userScore);
                }
            }

            
            await _context.SaveChangesAsync();

            return userArray;
        }

        public async Task<CleaningReport> CreateCleaningReportAsync(CleaningReportRequest request)
        {
            if (_context.CleaningReports == null)
            {
                throw new InvalidOperationException("CleaningReports set is null.");
            }

            if (request == null || request.CriteriaList == null || !request.CriteriaList.Any())
            {
                throw new ArgumentException("Invalid request data.");
            }

            // Kiểm tra báo cáo đã tồn tại trong ngày
            var existingReport = await _context.CleaningReports
                .Where(cr => cr.FormId == request.FormId
                             && cr.ShiftId == request.ShiftId
                             && EF.Functions.DateDiffDay(cr.CreateAt, DateTime.UtcNow) == 0)
                .FirstOrDefaultAsync();

            if (existingReport != null)
            {
                throw new InvalidOperationException("Form đã được đánh giá hôm nay.");
            }

            // Tạo bản ghi CleaningReport
            var cleaningReport = new CleaningReport
            {
                Id = Guid.NewGuid().ToString(),
                FormId = request.FormId,
                ShiftId = request.ShiftId,
                Value = 0,
                UserId = request.userId,
                CreateAt = GetCurrentTimeInGmtPlus7(),
                UpdateAt = GetCurrentTimeInGmtPlus7()
            };

            _context.CleaningReports.Add(cleaningReport);
            await _context.SaveChangesAsync();

            var reportId = cleaningReport.Id;

            // Lưu CriteriaReport
            foreach (var criteria in request.CriteriaList)
            {
                if (string.IsNullOrWhiteSpace(criteria.CriteriaId) || criteria.Value == null)
                {
                    continue;
                }

                var criteriaReport = new CriteriaReport
                {
                    Id = Guid.NewGuid().ToString(),
                    ReportId = reportId,
                    CriteriaId = criteria.CriteriaId,
                    Value = criteria.Value,
                    Note = criteria.Note,
                    CreateAt = GetCurrentTimeInGmtPlus7(),
                    UpdateAt = GetCurrentTimeInGmtPlus7(),
                    FormId = request.FormId,
                    ImageUrl = JsonConvert.SerializeObject(criteria.Images)
                };
                _context.CriteriaReports.Add(criteriaReport);
            }

            await _context.SaveChangesAsync();

            var criteriaReports = await _context.CriteriaReports
                .Where(cr => cr.ReportId == reportId)
                .ToListAsync();

            var criteriaList = await _context.Criteria
                .Where(c => criteriaReports.Select(cr => cr.CriteriaId).Contains(c.Id))
                .ToListAsync();

            var totalMaxValue = criteriaList.Sum(c => c.CriteriaType == "BINARY" ? 1 : 5);
            var totalValue = criteriaReports.Sum(cr => cr.Value ?? 0);

            var percentage = totalMaxValue > 0 ? (double)totalValue / totalMaxValue * 100 : 0;
            cleaningReport.Value = (int)Math.Round(percentage, 2);
            cleaningReport.UpdateAt = GetCurrentTimeInGmtPlus7();

            _context.CleaningReports.Update(cleaningReport);
            await _context.SaveChangesAsync();

            return cleaningReport;
        }

        public async Task<object> GetReportDetails(string reportId)
        {
            // Lấy báo cáo từ ReportId
            var report = await _context.CleaningReports.FirstOrDefaultAsync(x => x.Id == reportId);

            // Lấy Form
            var form = await _context.CleaningForms
                .FirstOrDefaultAsync(f => f.Id == report.FormId);
            if (form == null)
                return null;

            // Lấy Room và thông tin liên quan
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == form.RoomId);
            if (room == null)
                return null;

            var block = await _context.Blocks.FirstOrDefaultAsync(b => b.Id == room.BlockId);
            var floor = await _context.Floors.FirstOrDefaultAsync(f => f.Id == room.FloorId);
            var campus = await _context.Campuses.FirstOrDefaultAsync(c => c.Id == block.CampusId);
            var shift = await _context.Shifts.FirstOrDefaultAsync(s => s.Id == report.ShiftId);

            // Lấy criteria reports
            var criteriaList = await GetCriteriaList(reportId);

            // Lấy users theo tags
            var usersByTags = await GetUsersByTags(reportId);

            return new
            {
                Id = reportId,
                CampusName = campus?.CampusName,
                BlockName = block?.BlockName,
                FloorName = floor?.FloorName,
                RoomName = room?.RoomName,
                CriteriaList = criteriaList,
                CreateAt = report.CreateAt,
                UpdateAt = report.UpdateAt,
                ShiftName = shift?.ShiftName,
                StartTime = shift?.StartTime.ToString(),
                EndTime = shift?.EndTime.ToString(),
                UsersByTags = usersByTags
            };
        }

        private async Task<List<object>> GetCriteriaList(string reportId)
        {
            var criteriaPerReport = await _context.CriteriaReports
                .Where(cpf => cpf.ReportId == reportId)
                .ToListAsync();

            var criteriaList = new List<object>();
            foreach (var cpr in criteriaPerReport)
            {
                var criteria = await _context.Criteria
                    .Where(c => c.Id == cpr.CriteriaId)
                    .FirstOrDefaultAsync();

                criteriaList.Add(new
                {
                    Id = criteria?.Id,
                    Name = criteria?.CriteriaName,
                    CriteriaType = criteria?.CriteriaType,
                    Value = cpr.Value,
                    Note = cpr.Note,
                    ImageUrl = cpr.ImageUrl
                });
            }

            return criteriaList;
        }

        private async Task<List<object>> GetUsersByTags(string reportId)
        {
            var criteriaIds = await _context.CriteriaReports
                .Where(cr => cr.ReportId == reportId)
                .Select(c => c.CriteriaId)
                .ToListAsync();

            var userIds = await _context.UserScores
                .Where(us => us.ReportId == reportId)
                .Select(sd => sd.UserId)
                .Distinct()
                .ToListAsync();

            var tagIdsFromCriteria = await _context.TagsPerCriteria
                .Where(tpc => criteriaIds.Contains(tpc.CriteriaId))
                .Select(tpc => tpc.TagId)
                .Distinct()
                .ToListAsync();

            var tags = await _context.Tags
                .Where(t => tagIdsFromCriteria.Contains(t.Id))
                .ToListAsync();

            var userPerTags = await _context.UserPerTags
                .Where(upt => userIds.Contains(upt.UserId) && tagIdsFromCriteria.Contains(upt.TagId))
                .ToListAsync();

            var userResult = new List<object>();

            foreach (var tag in tags)
            {
                var usersInTag = userPerTags
                    .Where(upt => upt.TagId == tag.Id)
                    .Select(upt => upt.UserId)
                    .ToList();

                var users = await _context.Users
                    .Where(u => usersInTag.Contains(u.Id))
                    .Select(u => new
                    {
                        u.Id,
                        u.FirstName,
                        u.LastName,
                        u.UserName,
                        u.Email
                    })
                    .ToListAsync();

                userResult.Add(new
                {
                    TagId = tag.Id,
                    TagName = tag.TagName,
                    Users = users
                });
            }

            return userResult;
        }












        private DateTime GetCurrentTimeInGmtPlus7()
        {
           
            TimeZoneInfo gmtPlus7 = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gmtPlus7);
        }
    }
}
