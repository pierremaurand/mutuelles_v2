

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class CreditDto
    {
        public int Id { get; set; }
        [Required]
        public int MembreId { get; set; }
        [Required]
        public int Duree { get; set; }
        [Required]
        public float MontantCapital { get; set; }
		[Required]
        public float MontantCommission { get; set; }
        [Required]
        public float MontantInterets { get; set; }
        [Required]
        public string DateDemande { get; set; } = string.Empty;
		[Required]
        public string DateDecaissement { get; set; } = string.Empty;
    }
}