using Project.Entities;

namespace Project.Dto
{
    public class EditFormDto
    {
        public string FormId { get; set; }
        public List<Criteria> CriteriaList { get; set; }
    }

}
