using System;
using System.Collections.Generic;

namespace Project.Entities
{
    public partial class RoomSchedule
    {
        public RoomSchedule()
        {
            Events = new HashSet<Event>();
        }

        public string Id { get; set; } = null!;
        public string? ScheduleCode { get; set; }
        public int? YearBegin { get; set; }
        public short? Semester { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? SubjectCode { get; set; }
        public string? SubjectName { get; set; }
        public short? LessonDay { get; set; }
        public short? LessonStart { get; set; }
        public short? LessonEnd { get; set; }
        public int? RegisteredNumberOfStudent { get; set; }
        public string? TeacherCode { get; set; }
        public string? TeacherName { get; set; }
        public int? ExcelOrder { get; set; }
        public string? CreatorName { get; set; }
        public string? CreatorEmail { get; set; }
        public string? CreatorPhone { get; set; }
        public int? Status { get; set; }
        public string? RoomId { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
