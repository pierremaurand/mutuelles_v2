using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class MouvementComptableDto
    {
        public int Id { get; set; }
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