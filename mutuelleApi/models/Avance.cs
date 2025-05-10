

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Avance : BaseEntity
    {
        [Required]
        public int? MembreId { get; set; }
        [Required]
        public int? Duree { get; set; }
        [Required]
        public float MontantCapital { get; set; }
        [Required]
        public string? DateEnregistrement { get; set; }
        
        public Membre? Membre { get; set; }
        
    }
}