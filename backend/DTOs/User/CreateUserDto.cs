using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.User
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(6)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
