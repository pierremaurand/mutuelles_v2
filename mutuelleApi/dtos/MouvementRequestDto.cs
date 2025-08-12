namespace mutuelleApi.dtos
{
    public class MouvementRequestDto
    {
		[Required(ErrorMessage = "La date du mouvement est obligatoire!")]
        public string DateMouvement { get; set; } = string.Empty;
		[Required(ErrorMessage = "Le libellé du mouvement obligatoire!")]
        public string Libelle { get; set; } = string.Empty;
		[Required(ErrorMessage = "Le montant débit est obligatoire!")]
        public float MontantDebit { get; set; }
		[Required(ErrorMessage = "Le montant crédit est obligatoire!")]
        public float MontantCredit { get; set; }

        public int? MembreId { get; set; }
        public int? CotisationId { get; set; }
        public int? AdhesionId { get; set; }
        public int? AvanceId { get; set; }
        public int? CreditId { get; set; }
        public int? EcheanceId { get; set; }
    }
}