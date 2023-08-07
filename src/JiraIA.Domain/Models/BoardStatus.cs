namespace JiraIA.Domain.Models
{
    public class BoardStatus : BaseEntity
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
