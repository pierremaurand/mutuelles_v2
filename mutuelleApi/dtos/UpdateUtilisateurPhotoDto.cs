namespace mutuelleApi.dtos
{
    public class UpdateUtilisateurPhotoDto
    {
         [Required(ErrorMessage = "La photo est obligatoire")]
        public string Photo { get; set; } = string.Empty;
    }
}