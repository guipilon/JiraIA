using System.ComponentModel.DataAnnotations;

namespace JiraIA.Domain.DTOs
{
    public class BoardStatusDTO
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}
