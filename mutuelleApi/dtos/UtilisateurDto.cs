using System.ComponentModel.DataAnnotations;
using mutuelleApi.enums;

namespace mutuelleApi.dtos
{
    public class UtilisateurDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Le login est obligatoire!")]
        public string? Login { get; set; }
        [Required(ErrorMessage ="Le nom est obligatoire!")]
        public string? Nom { get; set; }
        [Required(ErrorMessage ="Le sexe est obligatoire!")]
        public Sexe? Sexe { get; set; }
        [Required(ErrorMessage ="Le role est obligatoire!")]
        public Role? Role { get; set; }
        public bool? EstActif { get; set; }
        public string? Photo { get; set; }
    }
}