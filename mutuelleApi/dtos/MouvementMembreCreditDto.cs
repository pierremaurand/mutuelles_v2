using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace mutuelleApi.dtos
{
    public class MouvementMembreCreditDto : BaseMouvementDto
    {
        public float MontantDebit { get; set; } = 0.0f;
        [Required(ErrorMessage = "Le montant crédit est obligatoire")]
        public float MontantCredit { get; set; } = 0.0f;
        [Required(ErrorMessage = "Le membre est obligatoire")]
        public int MembreId { get; set; }
    }
}