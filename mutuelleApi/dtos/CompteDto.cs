using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class CompteDto
    {
        public int Id { get; set; }
        [Required]
        public string? Numero { get; set; }
        [Required]
        public string? Libelle { get; set; }
    }
}