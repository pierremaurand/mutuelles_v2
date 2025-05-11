using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class GabaritDto
    {
        public int Id { get; set; }
        [Required]
        public string? Libelle { get; set; }
        public bool? EstActif { get; set; }
    }
}