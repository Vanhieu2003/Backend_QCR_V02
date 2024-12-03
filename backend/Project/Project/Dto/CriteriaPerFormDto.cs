using Project.Entities;

namespace Project.Dto
{
    public class CriteriaPerFormDto
    {
        public string? formId { get; set; }
        public List<Criteria> criteriaList { get; set; } = null!;
    }
}
