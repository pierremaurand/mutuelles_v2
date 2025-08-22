using System.ComponentModel.DataAnnotations.Schema;

namespace mutuelleApi.models
{
    public class Credit : BaseEntity
    {
        public int MembreId { get; set; }
        public int Duree { get; set; }
        public double MontantCapital { get; set; }
        public double MontantCommission { get; set; }
        public double MontantInterets { get; set; }
        public string DateDemande { get; set; } = string.Empty;
        public string DateDecaissement { get; set; } = string.Empty;

        public List<Echeance> Echeances { get; set; } = new List<Echeance>();
        public List<Mouvement>? Mouvements { get; set; }
        public Membre? Membre { get; set; }


        public double MontantTotal
        {
            get
            {
                return MontantCapital + MontantCommission + MontantInterets;
            }
        }

        public int NombreEcheancePaye => Echeances?.Count(e => e.MontantCapitalRestant == 0)??0;
		
		public int NombreEcheanceImpaye => Echeances?.Count(e => e.MontantCapitalRestant > 0)??0;
		
		public string DateDerniereEcheance => Echeances?.Max(e => e.DateEcheance)??"";

        public double MontantCapitalRestant => Echeances?.Sum(m => m.MontantCapitalRestant) ?? 0;

        public string Status => MontantCapitalRestant == 0 ? "Soldé" : "En cour";

        public void Decaisser(int modificateur)
        {
            if (!string.IsNullOrEmpty(DateDecaissement) && Mouvements is null)
            {
				Mouvements = new List<Mouvement>();
                var mouvement = new Mouvement();
                mouvement.DateMouvement = DateDecaissement;
                mouvement.Libelle = "Décaissement " + Libelle;
                mouvement.MontantDebit = MontantCapital;
                mouvement.MembreId = MembreId;
                mouvement.ModifiePar = modificateur;
                mouvement.ModifieLe = DateTime.Now;
                Mouvements.Add(mouvement);
            }
        }

        public string Libelle
        {
            get
            {
                return "crédit de " + MontantCapital + " du membre " + Membre?.Nom + " du " + DateDemande + " remboursable en " + Duree + " mois";
            }
        }

        public string Nom
        {
            get
            {
                return Membre?.Nom ?? "";
            }
        }

        public string NomSexe
        {
            get
            {
                return Membre?.NomSexe ?? "";
            }
        }

        public string Photo
        {
            get
            {
                return Membre?.Photo ?? "";
            }
        }

        public string NomAgence
        {
            get
            {
                return Membre?.NomAgence ?? "";
            }
        }

        public int AgenceId
        {
            get
            {
                return Membre?.AgenceId ?? 0;
            }
        }

        public int CreditId
        {
            get
            {
                return Id;
            }
        }

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