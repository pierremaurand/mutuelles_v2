using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class CotisationDto
    {
        public int Id { get; set; }
        [Required]
        public int MembreId { get; set; }
        [Required]
        public string? DateCotisation { get; set; }
        [Required]
        public float Montant { get; set; }
    }
}