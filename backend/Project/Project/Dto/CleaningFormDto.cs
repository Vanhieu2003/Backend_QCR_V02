namespace Project.Dto
{
    public class CreateCleaningFormRequest
    {
        public string FormName { get; set; }
        public List<RoomsDto> RoomId { get; set; }
        public List<CriteriasDto> CriteriaList { get; set; }
        public string? CreateAt { get; set; }
        public string? UpdateAt { get; set; }
    }

    public class RoomsDto
    {
        public string Id { get; set; }
    }

    public class CriteriasDto
    {
        public string Id { get; set; }
    }
}
