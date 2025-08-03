

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Echeance : BaseEntity
    {
        public string DateEcheance { get; set; } = string.Empty;
        public double MontantCapital { get; set; }
        public double MontantCommission { get; set; }
        public double MontantInterets { get; set; }

        public int? CreditId { get; set; }
        public int? AvanceId { get; set; }
        public string DatePaiement { get; set; } = string.Empty;
        public string DateAnticipation { get; set; } = string.Empty;

        public List<Mouvement>? Mouvements { get; set; } // Navigation property for movements
        public Credit? Credit { get; set; }
        public Avance? Avance { get; set; }

        public double MontantTotal => (MontantCapital + MontantCommission + MontantInterets);

        public double MontantRestant => MontantTotal - (Mouvements?.Sum(m => m.MontantCredit) ?? 0);

        public string Status => !string.IsNullOrEmpty(DatePaiement) && MontantRestant == 0 ? "Payée" : !string.IsNullOrEmpty(DateAnticipation) && MontantRestant == 0 ? "Anticipée" : "Impayée";

        public void Rembourser(int modificateur)
        {
            if (!string.IsNullOrEmpty(DatePaiement) && Mouvements is null)
            {
                Mouvements = new List<Mouvement>();
                var mouvement = new Mouvement();
                mouvement.DateMouvement = DatePaiement;
                mouvement.Libelle = "Remboursement capital échéance " + Libelle + " du " + DateEcheance;
                mouvement.MontantCredit = MontantCapital;
                mouvement.MembreId = Avance is null ? (Credit is null ? 0 : Credit.MembreId) : Avance.MembreId;
				mouvement.CreditId = Credit?.Id;
				mouvement.AvanceId = Avance?.Id;
                mouvement.ModifiePar = modificateur;
                mouvement.ModifieLe = DateTime.Now;
                Mouvements.Add(mouvement);
                
                if(Credit is not null) {
					mouvement = new Mouvement();
					mouvement.DateMouvement = DatePaiement;
					mouvement.Libelle = "Remboursement intérêts échéance " + Libelle + " du " + DateEcheance ;
					mouvement.MontantCredit = MontantInterets;
					mouvement.ModifiePar = modificateur;
					mouvement.ModifieLe = DateTime.Now;
					Mouvements.Add(mouvement);
				}
            }
        }

        private string Libelle => (Avance is null ? (Credit is null ? "": Credit.Libelle) : Avance.Libelle);
        
		public string NomMembre => Avance?.Membre?.Nom ?? Credit?.Membre?.Nom ?? "";
		public string SexeMembre => Avance?.Membre?.NomSexe ?? Credit?.Membre?.NomSexe ?? "";
		public string PhotoMembre => Avance?.Membre?.Photo ?? Credit?.Membre?.Photo ?? "";
		public int AgenceId => Avance?.Membre?.AgenceId ?? Credit?.Membre?.AgenceId ?? 0;
    }
}