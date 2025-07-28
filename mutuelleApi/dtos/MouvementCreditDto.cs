using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.dtos
{
    public class MouvementCreditDto : BaseMouvementDto
    {
        [Required(ErrorMessage = "Le montant débité est obligatoire")]
        public float MontantDebit { get; set; } = 0.0f;
        public float MontantCredit { get; set; }  = 0.0f;
        [Required(ErrorMessage = "Le credit est obligatoire")]
        public int CreditId { get; set; }
    }   
}