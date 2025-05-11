using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class OperationDto
    {
        public int Id { get; set; }
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
    }
}