using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.dtos
{
    public class MouvementAvanceDto : BaseMouvementDto
    {
        [Required(ErrorMessage = "Le montant débité est obligatoire")]
        public float MontantDebit { get; set; } = 0.0f;
        public float MontantCredit { get; set; } = 0.0f;
        [Required(ErrorMessage = "L'avance est obligatoire")]
        public int AvanceId { get; set; }
    }
}