namespace mutuelleApi.dtos
{
    public class EcheanceDto
    {
        public int Id { get; set; }
        public string DateEcheance { get; set; } = string.Empty;
        public double MontantCapital { get; set; }
        public double MontantCommission { get; set; } = 0;
        public double MontantInterets { get; set; } = 0;


        public double MontantTotal { get; set; } = 0;
        public double MontantRestant { get; set; } = 0;
        public string Status { get; set; }  = string.Empty;

        public int CreditId { get; set; } = 0;
        public int AvanceId { get; set; } = 0;
		
		public string NomMembre { get; set; } = string.Empty;
		public string SexeMembre { get; set; } = string.Empty;
		public string PhotoMembre { get; set; } = string.Empty;
		public string NomAgence { get; set; } = string.Empty;
		public string UtilisateurLogin { get; set; } = string.Empty;
       
    }
}