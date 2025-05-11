

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Echeance : BaseEntity
    {
        [Required]
        public string? DateEcheance { get; set; }
        [Required]
        public float MontantCapital { get; set; }
        public float MontantInterets { get; set; }

        public int? CreditId { get; set; }
        public int? AvanceId { get; set; }
    }
}