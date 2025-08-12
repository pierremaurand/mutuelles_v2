namespace mutuelleApi.models
{
    public class Adhesion : BaseEntity
    {
        public int MembreId { get; set; }
        public double Montant { get; set; }
        public string DateAdhesion { get; set; } = string.Empty;

        public Membre? Membre { get; set; }
        public Mouvement? Mouvement { get; set; }

        public string Libelle => "adhésion du membre " + Membre?.Nom;
        
        public void Payer(int modificateur)
        {
            if (!string.IsNullOrEmpty(DateAdhesion) && Mouvement is null)
            {
                var mouvement = new Mouvement();
                mouvement.DateMouvement = DateAdhesion;
                mouvement.Libelle = "Paiement " + Libelle;
                mouvement.MontantCredit = Montant;
                mouvement.ModifiePar = modificateur;
                mouvement.ModifieLe = DateTime.Now;
                Mouvement = mouvement;
            }
        }
		
		public string NomMembre => Membre?.Nom ?? "";
		public string SexeMembre => Membre?.NomSexe ?? "";
		public string PhotoMembre => Membre?.Photo ?? "";
		public int AgenceId => Membre?.AgenceId ?? 0;
		
		[ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
    }
}