using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.dtos
{
    public class UpdateUtilisateurPhotoDto
    {
         [Required(ErrorMessage = "La photo est obligatoire")]
        public string Photo { get; set; } = string.Empty;
    }
}