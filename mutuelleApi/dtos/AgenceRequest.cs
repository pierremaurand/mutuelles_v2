using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class AgenceRequest
    {
        [Required(ErrorMessage = "Le nom de l'agence est obligatoire!")]
        public string Nom { get; set; } = string.Empty;
    }
}