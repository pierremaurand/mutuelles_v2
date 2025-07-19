using System.ComponentModel.DataAnnotations;
using mutuelleApi.enums;

namespace mutuelleApi.dtos
{
    public class MembreDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public Sexe Sexe { get; set; } 
        public string DateNaissance { get; set; } = string.Empty;
        public string LieuNaissance { get; set; } = string.Empty;
        public int AgenceId { get; set; }
        public string DateAdhesion { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public bool EstActif { get; set; } = true;
    }
}