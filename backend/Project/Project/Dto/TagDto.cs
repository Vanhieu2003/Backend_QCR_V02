namespace Project.Dto
{
    public class TagAverageRatingDto
    {
        public string TagName { get; set; }
        public double AverageRating { get; set; }
    }

    public class TagGroupDto
    {
        public string Id { get; set; }
        public string TagName { get; set; }
        public int NumberOfUsers { get; set; }
    }

    public class ResponsibleTagDto
    {
        public string Id { get; set; }
        public string GroupName { get; set; }
        public string TagName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }

}
