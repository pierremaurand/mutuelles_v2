

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Agence : BaseEntity
    {
        [Required]
        public string? Nom { get; set; }
    }
}