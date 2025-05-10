

using System.ComponentModel.DataAnnotations;
using mutuelleApi.enums;

namespace mutuelleApi.models
{
    public class Membre : BaseEntity
    {
        [Required]
        public string? Nom { get; set; }
        [Required]
        public Sexe? Sexe { get; set; }
        [Required]
        public string? DateNaissance { get; set; }
        [Required]
        public string? LieuNaissance { get; set; }
        [Required]
        public int? AgenceId { get; set; }
        [Required]
        public string? DateAdhesion { get; set; }
        [Required]
        public string? Telephone { get; set; }
        [Required]
        public string? Email { get; set; }

        public string? Photo { get; set; }
        public bool? EstActif { get; set; }
        public Agence? Agence { get; set; }
    }
}