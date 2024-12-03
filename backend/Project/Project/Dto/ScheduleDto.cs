namespace Project.Dto
{
    public class ScheduleDto
    {
        public string Subject { get; set; }
        public List<string> Users { get; set; }
        public string ResponsibleGroupId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay { get; set; }
        public List<PlaceDTO> Place { get; set; }
        public string? RecurrenceRule { get; set; }
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? customId { get; set; }
    }
    public class PlaceItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class PlaceDTO
    {
       public string level { get; set; }
       
       public List<PlaceItemDTO> rooms { get; set; }
    }


    public class ScheduleDetailInfoDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public string RecurrenceRule { get; set; }
        public string? ResponsibleGroupId { get; set; }
        public bool AllDay { get; set; }
        public List<UserDto> Users { get; set; }

        public List<PlaceDTO> Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Index { get; set; }

    }
    public class RoomItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class ResponsibleUserDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class ScheduleUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RecurrenceRule { get; set; }
        public string? ResponsibleGroupId { get; set; }
        public bool? AllDay { get; set; }
        public List<string>? Users { get; set; } 
        public List<PlaceDTO>? Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Index { get; set; }
    }


    public class ScheduleCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ResponsibleGroupId { get; set; }
        public bool? AllDay { get; set; }
        public List<string>? Users { get; set; } 
        public List<PlaceDTO>? Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? RecurrenceRule { get; set; }
        public int? Index { get; set; }
    }

    public class ShiftRoomCriteriaRequest
    {
        public string ShiftId { get; set; }
        public string RoomId { get; set; }
        public List<int> CriteriaIds { get; set; }
    }
}
