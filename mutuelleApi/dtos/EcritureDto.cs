using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class EcritureDto
    {
        public int Id { get; set; }
        [Required]
        public string? DateEnregistrement { get; set; }
        [Required]
        public string? Libelle { get; set; }
        [Required]
        public int? MouvementId { get; set; }
    }
}