

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Ecriture : BaseEntity
    {
        [Required]
        public string? DateEnregistrement { get; set; }
        [Required]
        public string? Libelle { get; set; }
        [Required]
        public int? MouvementId { get; set; }
    }
}