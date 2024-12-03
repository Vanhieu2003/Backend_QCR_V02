namespace Project.Dto
{
    public class RoomDto
    {
        public string? Id { get; set; } 
        public string? RoomName { get; set; } 
        public string? FloorId { get; set; } 
        public string? BlockId { get; set; } 
        public string? RoomCategoryId { get; set; } 
    }


    public class RoomViewDto
    {
        public string? Id { get; set; }
        public string? RoomName { get; set; }
        public string? CampusName { get; set; }
        public string? CampusId { get; set; }
        public string? FloorName{ get; set; }
        public string? BlockName { get; set; }
        public string? CategoryName { get; set; }
    }
}
