using System.ComponentModel.DataAnnotations.Schema;

namespace mutuelleApi.models
{
    public class Echeance : BaseEntity
    {
        public string DateEcheance { get; set; } = string.Empty;
        public double MontantCapital { get; set; }
        public double MontantCommission { get; set; }
        public double MontantInterets { get; set; }

        public int? CreditId { get; set; }
		public Credit? Credit { get; set; }
        public int? AvanceId { get; set; }
		public Avance? Avance { get; set; }
		
        public string? DatePaiement { get; set; } 
        public string? DateAnticipation { get; set; }

        public List<Mouvement>? Mouvements { get; set; }

        public double MontantTotal
        {
            get
            {
                return MontantCapital + MontantCommission + MontantInterets;
            }
        }

        public double MontantCapitalRestant
        {
            get
            {
                return MontantCapital - ((Mouvements?.Sum(m => m.MontantCredit) ?? 0)- MontantInterets- MontantCommission);
            }
        }

        public string Status
        {
            get
            {
                return MontantCapitalRestant > 0 ? "Impayée" : (!string.IsNullOrEmpty(DatePaiement) ? "Payée" : "Anticipée");
            }
        }

        public void Rembourser(int modificateur)
        {
			if(!string.IsNullOrEmpty(DateAnticipation) ^ !string.IsNullOrEmpty(DatePaiement)) {
				string? dateMouvement = string.IsNullOrEmpty(DateAnticipation) ? DatePaiement: DateAnticipation;
			
				double montantDebit = 0;
				
				int membreId = Avance is null ? (Credit is null ? 0 : Credit.MembreId) : Avance.MembreId;
				
				int? creditId = Credit?.Id;
				int? avanceId = Avance?.Id;
				
				//Remboursement échéance capital avance credit
				double montantCredit = MontantCapital; 
				string libelle = "Remboursement capital échéance " + Libelle + " du " + DateEcheance;
				
				AddMouvement(dateMouvement,libelle,montantDebit, montantCredit, membreId, creditId, avanceId, modificateur);
				  
				if(Credit is not null) {
					//Remboursement échéance commission
					montantCredit = MontantCommission;
					libelle = "Remboursement commission échéance " + Libelle + " du " + DateEcheance;
					
					AddMouvement(dateMouvement,libelle,montantDebit, montantCredit, membreId,creditId, avanceId, modificateur);
					
					//Remboursement échéance intérêts
					montantCredit = MontantInterets;
					libelle = "Remboursement intérêts échéance " + Libelle + " du " + DateEcheance;
					
					AddMouvement(dateMouvement,libelle,montantDebit, montantCredit, membreId,creditId, avanceId, modificateur);
				}
			}
        }
		
		private void AddMouvement(string? dateMouvement, string libelle, double montantDebit, double montantCredit, int membreId, int? creditId, int? avanceId, int modificateur) {
			if(Mouvements is null) {
				Mouvements = new List<Mouvement>();
			}
			
			var mouvement = new Mouvement();
			mouvement.DateMouvement = dateMouvement??string.Empty;
			mouvement.Libelle = libelle;
			mouvement.MontantCredit = montantCredit;
			mouvement.MontantDebit = montantDebit;
			mouvement.MembreId = membreId;
			mouvement.CreditId = creditId;
			mouvement.AvanceId = avanceId;
			mouvement.ModifiePar = modificateur;
			mouvement.ModifieLe = DateTime.Now;
			Mouvements.Add(mouvement);
		}

        private string Libelle
        {
            get
            {
                return Avance?.Libelle ?? Credit?.Libelle ?? "";
            }
        }

        public string Nom
        {
            get
            {
                return Avance?.Nom ?? Credit?.Nom ?? "";
            }
        }

        public string NomSexe
        {
            get
            {
                return Avance?.NomSexe ?? Credit?.NomSexe ?? "";
            }
        }

        public string Photo
        {
            get
            {
                return Avance?.Photo ?? Credit?.Photo ?? "";
            }
        }

        public string NomAgence
        {
            get
            {
                return Avance?.NomAgence ?? Credit?.NomAgence ?? "";
            }
        }

        public int AgenceId
        {
            get
            {
                return Avance?.AgenceId ?? Credit?.AgenceId ?? 0;
            }
        }

        public int EcheanceId => Id;

        [ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }

        public string UtilisateurLogin
        {
            get
            {
                return Utilisateur?.Login ?? "";
            }
        }
    }
}