namespace mutuelleApi.models
{
    public class Avance : BaseEntity
    {
        public int MembreId { get; set; }
        public int Duree { get; set; }
        public double MontantCapital { get; set; }
        public string DateDemande { get; set; } = string.Empty;
        public string DateDecaissement { get; set; } = string.Empty;

        public List<Echeance>? Echeances { get; set; }
        public List<Mouvement>? Mouvements { get; set; }
        public Membre? Membre { get; set; }
		
		public string NomMembre => Membre?.Nom ?? "";
		public string SexeMembre => Membre?.NomSexe ?? "";
		public string PhotoMembre => Membre?.Photo ?? "";
		public string NomAgence => Membre?.NomAgence ?? "";
		public int AgenceId => Membre?.AgenceId ?? 0;

        public double MontantTotal => (MontantCapital);

		public int NombreEcheancePaye => (Echeances?.Count(e => e.MontantRestant == 0)??0);
		
		public int NombreEcheanceImpaye => (Echeances?.Count(e => e.MontantRestant > 0)??0);
		
		public string DateDerniereEcheance => (Echeances?.Max(e => e.DateEcheance)??"");
		
        public double MontantRestant => (Echeances?.Sum(m => m.MontantRestant) ?? 0);

        public string Status => MontantRestant == 0 ? "Remboursée" : "En cour";

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
        
        public string Libelle => "avance de " + MontantCapital + " du membre " + Membre?.Nom + " du " + DateDemande + " remboursable en " + Duree + " mois";
		
		[ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
     
    }
}