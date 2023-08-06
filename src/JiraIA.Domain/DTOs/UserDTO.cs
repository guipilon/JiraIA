using System.ComponentModel.DataAnnotations;

namespace JiraIA.Domain.DTOs
{
    public class UserDTO
    {
        public string? Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Role { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
