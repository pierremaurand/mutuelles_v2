using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class UploadImage
    {
        [Required(ErrorMessage = "L'image est obligatoire!")]
        public string? Image { get; set; }
        [Required(ErrorMessage ="L'extension de l'image est obligatoire")]
        public string? Extension { get; set; }
    }
}