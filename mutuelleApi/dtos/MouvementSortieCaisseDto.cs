namespace mutuelleApi.dtos
{
    public class MouvementSortieCaisseDto : BaseMouvementDto
    {
        [Required(ErrorMessage = "Le montant débité est obligatoire")]
        public float MontantDebit { get; set; } = 0.0f;
        public float MontantCredit { get; set; } = 0.0f;
    }
}