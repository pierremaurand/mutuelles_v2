

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Gabarit : BaseEntity
    {
        [Required]
        public string? Libelle { get; set; }
        public bool? EstActif { get; set; }
    }
}