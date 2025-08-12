using System.ComponentModel.DataAnnotations.Schema;

namespace mutuelleApi.models
{
    public class Mouvement : BaseEntity
    {
        public string DateMouvement { get; set; } = string.Empty;
        public string Libelle { get; set; } = string.Empty;
        public double MontantDebit { get; set; } = 0;
        public double MontantCredit { get; set; } = 0;

        public int? MembreId { get; set; }
        public int? CotisationId { get; set; }
        public int? AdhesionId { get; set; }
        public int? AvanceId { get; set; }
        public int? CreditId { get; set; }
        public int? EcheanceId { get; set; }
		
		[ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
    }
}