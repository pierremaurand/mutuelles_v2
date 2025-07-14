

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage ="Le mot de passe est obligatoire!")]
        public string? MotDePasse { get; set; }
        [Required(ErrorMessage ="La confirmation du mot de passe est obligatoire!")]
        public string? ConfirmMotDePasse { get; set; }
    }
}