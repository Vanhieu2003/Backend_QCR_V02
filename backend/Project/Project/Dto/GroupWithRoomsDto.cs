namespace Project.Dto
{
    public class GroupWithRoomsDto
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }

    public class ResponsiableGroupDto
    {

        public string? Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public List<UserDto>? Users { get; set; }
    }

    public class ResponsibleGroupUpdateDto
    {
        public string? GroupName { get; set; } 
        public string? Description { get; set; }  
        public string? Color { get; set; }  
        public List<UserDto> Users { get; set; }  
    }





    public class GroupWithRoomsViewDto
    {
        public string? Id { get; set; }
        public string? CampusName { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public int? NumberOfRoom { get; set; }
    }

    public class ResponsiableGroupViewDto
    {
        public string? Id { get; set; }
        public string? GroupName { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public int NumberOfUser { get; set; }
    }


    public class RoomGroupViewDto
    {
        public string? Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public List<RoomViewDto> Rooms { get; set; }
    }

    public class RoomGroupUpdateDto
    {
        public string? Id { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }


    

    



}
