using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.dtos
{
    public class MouvementEcheanceDto : BaseMouvementDto
    {
        public float MontantDebit { get; set; } = 0.0f;
        [Required(ErrorMessage = "Le montant crédité est obligatoire")]
        public float MontantCredit { get; set; } = 0.0f;
        [Required(ErrorMessage = "L'écheance est obligatoire")]
        public int EcheanceId { get; set; }
    }
}