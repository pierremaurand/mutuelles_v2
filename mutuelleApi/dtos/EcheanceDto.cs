using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class EcheanceDto
    {
        public int Id { get; set; }
        public string DateEcheance { get; set; } = string.Empty;
        public float MontantCapital { get; set; }
        public float MontantCommission { get; set; } = 0.0f;
        public float MontantInterets { get; set; } = 0.0f;


        public float MontantTotal { get; set; } = 0.0f;
        public float MontantRestant { get; set; } = 0.0f;
        public string Status { get; set; }  = string.Empty;

        public int CreditId { get; set; } = 0;
        public int AvanceId { get; set; } = 0;
       
    }
}