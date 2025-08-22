using System.ComponentModel.DataAnnotations.Schema;

namespace mutuelleApi.models
{
    public class Cotisation : BaseEntity
    {
        public int MembreId { get; set; }
        public string DateCotisation { get; set; } = string.Empty;
        public double Salaire { get; set; } = 0;
        public double Retenue => Math.Round(Salaire * 0.05);

        public Membre? Membre { get; set; }
        public Mouvement? Mouvement { get; set; }


        public string Libelle
        {
            get
            {
                return "cotisation du membre " + Membre?.Nom + " du " + DateCotisation;
            }
        }

        public void Payer(int modificateur)
        {
            if (!string.IsNullOrEmpty(DateCotisation) && Mouvement is null)
            {
                var mouvement = new Mouvement();
                mouvement.DateMouvement = DateCotisation;
                mouvement.Libelle = "Paiement " + Libelle;
                mouvement.MontantCredit = Retenue;
                mouvement.MembreId = MembreId;
                mouvement.ModifiePar = modificateur;
                mouvement.ModifieLe = DateTime.Now;
                Mouvement = mouvement;
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

        public int CotisationId
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