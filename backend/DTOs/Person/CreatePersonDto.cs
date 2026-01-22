using System.ComponentModel.DataAnnotations;

namespace backend.DTOs.Person
{
    public class CreatePersonDto
    {
        [MinLength(2)]
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Phone { get; set; } = string.Empty;
    }
}