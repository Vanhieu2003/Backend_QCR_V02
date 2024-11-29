namespace Project.Dto
{
    public class UsersPerTagDto
    {
    }
    public class AssignUserRequest
    {
        public string TagId { get; set; }
        public List<string> Id { get; set; }
        public string Type { get; set; }
    }
}
