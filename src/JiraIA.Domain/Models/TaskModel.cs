namespace JiraIA.Domain.Models
{
    public class TaskModel : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public string AssignedTo { get; set; }

        public string Status { get; set; }

        public bool IsFavorited { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
