using System.ComponentModel.DataAnnotations;

namespace JiraIA.Domain.DTOs
{
    public class TaskDTO
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Deadline { get; set; }

        public string? AssignedTo { get; set; }

        public string? Status { get; set; }

        public bool IsFavorited { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
