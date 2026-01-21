using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.User
{
    public class UpdateUserDto
    {
        [Required]
        [MinLength(6)]
        public string UserName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
