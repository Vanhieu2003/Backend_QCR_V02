using Project.Entities;
using System.Runtime.ConstrainedExecution;

namespace Project.Dto
{
    public class CriteriaDto
    {
        public string? CriteriaId { get; set; }
        public List<Tag> Tag { get; set; } = null!;
    }
    public class CreateCriteriaDto
    {
        public string CriteriaName { get; set; } = null!;
        public string RoomCategoryId { get; set; } = null!;
        public string CriteriaType { get; set; } = null!;
        public List<string> Tags { get; set; } = new List<string>();
    }
    public class CriteriaReportDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CriteriaType { get; set; }
        public int Value { get; set; }
        public string Note {get;set;}
        public string ImageUrl { get; set; }
        

    }
}
