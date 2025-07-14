using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using mutuelleApi.enums;

namespace mutuelleApi.dtos
{
    public class UtilisateurRequestDto
    {
        [Required(ErrorMessage = "Le login est obligatoire!")]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage ="Le nom est obligatoire!")]
        public string Nom { get; set; } = string.Empty;
        [Required(ErrorMessage ="Le sexe est obligatoire!")]
        public Sexe Sexe { get; set; }
        [Required(ErrorMessage ="Le role est obligatoire!")]
        public Role Role { get; set; }
    }
}