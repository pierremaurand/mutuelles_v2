using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class AdhesionRequestDto
    {
        [Required(ErrorMessage = "Le membre est obligatoire!")]
        public int MembreId { get; set; } 
        [Required(ErrorMessage = "Le montant est obligatoire!")]
        public float Montant { get; set; } 
    }
}