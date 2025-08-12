namespace mutuelleApi.dtos
{
    public class MouvementEntreCaisseDto : BaseMouvementDto
    {
        public float MontantDebit { get; set; } = 0.0f;
         [Required(ErrorMessage = "Le montant crédité est obligatoire")]
        public float MontantCredit { get; set; } = 0.0f;
    }
}