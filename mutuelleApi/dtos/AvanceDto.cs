using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class AvanceDto
    {
        public int Id { get; set; }
        [Required]
        public int? MembreId { get; set; }
        [Required]
        public int? Duree { get; set; }
        [Required]
        public float MontantCapital { get; set; }
        [Required]
        public string? DateEnregistrement { get; set; }
    }
}