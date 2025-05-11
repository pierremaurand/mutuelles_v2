

using System.ComponentModel.DataAnnotations;
using mutuelleApi.enums;

namespace mutuelleApi.models
{
    public class Utilisateur : BaseEntity
    {
        [Required]
        public string? Login { get; set; }
        [Required]
        public byte[]? MotDePasse { get; set; }
        [Required]
        public byte[]? ClesMotDePasse { get; set; }
        [Required]
        public Role? Role { get; set; }
        public bool? EstActif { get; set; }
        public string? Photo { get; set; }
    }
}