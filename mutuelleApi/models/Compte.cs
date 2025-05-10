

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Compte : BaseEntity
    {
        [Required]
        public string? Numero { get; set; }
        [Required]
        public string? Libelle { get; set; }
    }
}