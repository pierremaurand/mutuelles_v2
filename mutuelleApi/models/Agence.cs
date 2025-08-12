using System.ComponentModel.DataAnnotations.Schema;

namespace mutuelleApi.models
{
    public class Agence : BaseEntity
    {
        public string Nom { get; set; } = string.Empty;
		
		[ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
    }
}