

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Mouvement : BaseEntity
    {
        [Required]
        public string? DateMouvement { get; set; }
        [Required]
        public string? Libelle { get; set; }
        [Required]
        public float MontantDebit { get; set; }
        [Required]
        public float MontantCredit { get; set; }

        public int? MembreId { get; set; }
        public int? CotisationId { get; set; }
        public int? AvanceId { get; set; }
        public int? CreditId { get; set; }
        public int? EcheanceId { get; set; }

        public Membre? Membre { get; set; }
        public Cotisation? Cotisation { get; set; }
        public Avance? Avance { get; set; }
        public Credit? Credit { get; set; }
        public Echeance? Echeance { get; set; }
    }
}