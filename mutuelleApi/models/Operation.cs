

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Operation : BaseEntity
    {
        [Required]
        public int? GabaritId { get; set; }
        [Required]
        public int? CompteId { get; set; }
        [Required]
        public bool? MontantFixe { get; set; }
        [Required]
        public float MontantDebit { get; set; }
        [Required]
        public float MontantCredit { get; set; }

        public Gabarit? Gabarit { get; set; }
        public Compte? Compte { get; set; }
    }
}