using mutuelleApi.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace mutuelleApi.models
{
    public class Membre : BaseEntity
    {
        public string Nom { get; set; } = string.Empty;
        public Sexe Sexe { get; set; }
        public string DateNaissance { get; set; } = string.Empty;
        public string LieuNaissance { get; set; } = string.Empty;
        public int AgenceId { get; set; }
        public Agence? Agence { get; set; } // Navigation property
        public string DateAdhesion { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<Mouvement>? Mouvements { get; set; } // Navigation property for movements
        public string Photo { get; set; } = string.Empty;
        public bool EstActif { get; set; } = true;

        public double MontantCotise => (Mouvements?.FindAll(m => m.CotisationId is not null).Sum(m => m.MontantCredit)??0);

        public double Solde => (Mouvements?.Sum(m => m.MontantCredit - m.MontantDebit))??0;

        public void Encaisser(int modificateur)
        {

        }

        public void Decaisser(int modification)
        {

        }

        public void Solder(int modificateur)
        {
            
        }

        public string NomAgence => Agence?.Nom??"";
		
		public string NomSexe => Sexe == Sexe.Masculin ? "Homme":"Femme";

        public int MembreId => Id;
		
		[ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
        
    }
}