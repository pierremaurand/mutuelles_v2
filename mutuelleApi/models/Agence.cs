
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Agence : BaseEntity
    {
        [Required(ErrorMessage = "Le nom de l'agence est obligatoire!")]
        public string Nom { get; set; } = string.Empty;
		
		[ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
    }
}