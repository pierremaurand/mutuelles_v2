using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class EcheanceDto
    {
        public int Id { get; set; }
        [Required]
        public string DateEcheance { get; set; } = string.Empty;
        [Required]
        public float MontantCapital { get; set; }
        public float MontantCommission { get; set; }
        public float MontantInterets { get; set; }

        public int CreditId { get; set; }
        public int AvanceId { get; set; }
		public bool EstPaye { get; set; }
    }
}