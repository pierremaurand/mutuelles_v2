using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using mutuelleApi.enums;
using mutuelleApi.validators;

namespace mutuelleApi.dtos
{
    public class UtilisateurRequestDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le login est obligatoire!")]
        [LoginUtilisateurUniqueAttribute(ErrorMessage = "Le login doit être unique!")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le nom est obligatoire!")]
        public string Nom { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le sexe est obligatoire!")]
        public Sexe Sexe { get; set; }
        [Required(ErrorMessage = "Le role est obligatoire!")]
        public Role Role { get; set; }
        
        public string Photo { get; set; } = string.Empty;
    }
}