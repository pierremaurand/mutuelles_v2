using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.dtos
{
    public class CotisationRequestDto
    {
        [Required(ErrorMessage = "Le membre est obligatoire!")]
        public int MembreId { get; set; }
        [Required(ErrorMessage = "La date de la cotisation est obligatoire!")]
        public string DateCotisation { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le montant de la cotisation est obligatoire!")]
        public float Montant { get; set; }
    }
}