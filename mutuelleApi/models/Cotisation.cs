

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Cotisation : BaseEntity
    {
        [Required]
        public int MembreId { get; set; }
        [Required]
        public string? DateCotisation { get; set; }
        [Required]
        public float Montant { get; set; }
    }
}