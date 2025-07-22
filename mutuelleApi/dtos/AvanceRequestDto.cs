using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class AvanceRequestDto
    {
        [Required(ErrorMessage = "Le membre est obligatoire!")]
        public int MembreId { get; set; }
        [Required(ErrorMessage = "La durée est obligatoire!")]
        public int Duree { get; set; }
        [Required(ErrorMessage = "Le montant du capital est obligatoire!")]
        public float MontantCapital { get; set; }
        [Required(ErrorMessage = "La date d'enregistremet est obligatoire!")]
        public string DateEnregistrement { get; set; } = string.Empty;
    }
}