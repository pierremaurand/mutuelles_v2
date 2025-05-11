

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class MouvementComptable : BaseEntity
    {
        [Required]
        public int? EcritureId { get; set; }
        [Required]
        public int? CompteId { get; set; }
        [Required]
        public float MontantDebit { get; set; }
        [Required]
        public float MontantCredit { get; set; }
    }
}