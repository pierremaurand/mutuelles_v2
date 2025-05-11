using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class AuthRequestDto
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}