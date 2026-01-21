using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Person
{
    public class CreatePersonDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}