using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class AvanceDto
    {
        public int Id { get; set; }
        [Required]
        public int MembreId { get; set; }
        [Required]
        public int Duree { get; set; }
        [Required]
        public float MontantCapital { get; set; }
        [Required]
        public string DateDemande { get; set; } = string.Empty;
		[Required]
        public string DateDecaissement { get; set; } = string.Empty;
    }
}