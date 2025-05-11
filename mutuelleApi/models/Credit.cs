

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Credit : BaseEntity
    {
        [Required]
        public int? MembreId { get; set; }
        [Required]
        public int? Duree { get; set; }
        [Required]
        public float MontantCapital { get; set; }
        [Required]
        public float MontantInterets { get; set; }
        [Required]
        public string? DateEnregistrement { get; set; }
    }
}