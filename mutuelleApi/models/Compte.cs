namespace mutuelleApi.models
{
    public class Compte : BaseEntity
    {
        [Required]
        public string? Numero { get; set; }
        [Required]
        public string? Libelle { get; set; }
		
		[ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
    }
}