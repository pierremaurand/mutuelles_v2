using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.dtos
{
    public class BaseMouvementDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La date de l'opération est obligatoire")]
        public string DateMouvement { get; set; } = string.Empty;
         [Required(ErrorMessage = "Le libellé de l'opération est obligatoire")]
        public string Libelle { get; set; } = string.Empty;
    }
}