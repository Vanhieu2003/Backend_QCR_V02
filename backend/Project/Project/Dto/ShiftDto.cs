namespace Project.Dto
{
    public class ShiftDto
    {
        public string? Id { get; set; }
        public string ShiftName { get; set; } = null!;
        public string StartTime { get; set; } = null!;
        public string EndTime { get; set; } = null!;
        public string? RoomCategoryId { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }


    public class ShiftViewDto
    {
        public string? Id { get; set; }
        public string ShiftName { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? CategoryName { get; set; }
        public string? roomCategoryId { get; set; }
        public string Status { get; set; }
    }


    public class ShiftCreateDto
    {

        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<string> Category { get; set; }
    }


    public class ShiftUpdateDto
    {
        public string? Id { get; set; }

        public string ShiftName { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public List<string> Category { get; set; }

        public string Status { get; set; }
    }


}
