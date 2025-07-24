

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class CreditRequestDto
    {
        [Required(ErrorMessage = "Le membre est obligatoire!")]
        public int MembreId { get; set; }
        [Required(ErrorMessage = "La durée est obligatoire!")]
        public int Duree { get; set; }
        [Required(ErrorMessage = "Le montant du capital est obligatoire!")]
        public float MontantCapital { get; set; }
		[Required(ErrorMessage = "Le montant de la commission est obligatoire!")]
        public float MontantCommission { get; set; }
        [Required(ErrorMessage = "Le montant des intérêts est obligatoire!")]
        public float MontantInterets { get; set; }
        [Required(ErrorMessage = "La date de la demande est obligatoire!")]
        public string DateDemande { get; set; } = string.Empty;
		[Required(ErrorMessage = "La date de décaissement est obligatoire!")]
        public string DateDecaissement { get; set; } = string.Empty;
    }
}