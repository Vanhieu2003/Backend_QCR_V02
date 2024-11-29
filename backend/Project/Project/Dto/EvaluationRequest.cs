namespace Project.Dto
{
    public class EvaluationRequest
    {
        public string ReportId { get; set; }
        public string ShiftId { get; set; }
        public int Value { get; set; }
        public string UserId { get; set; }
        public List<CriteriaRequesttest> CriteriaList { get; set; }
        public List<UserPerTagRequest> UserPerTags { get; set; }
    }

    public class CriteriaRequesttest
    {
        public string CriteriaId { get; set; }
        public int Value { get; set; }
        public string Note { get; set; }
        public object Images { get; set; }
    }

    public class UserPerTagRequest
    {
        public string TagId { get; set; }
        public string TagName { get; set; }
        public List<UserRequest> Users { get; set; }
    }

    public class UserRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

}
