namespace mutuelleApi.dtos
{
    public class CreditDto
    {
        public int Id { get; set; }
        public int Duree { get; set; }
        public double MontantCapital { get; set; }
        public double MontantCommission { get; set; }
        public double MontantInterets { get; set; }
        public string DateDemande { get; set; } = string.Empty;
        public string DateDecaissement { get; set; } = string.Empty;
        public double MontantTotal { get; set; } = 0;
        public double MontantRestant { get; set; } = 0;
		
        public string Status { get; set; }  = string.Empty;
		public string NomMembre { get; set; } = string.Empty;
		public string SexeMembre { get; set; } = string.Empty;
		public string PhotoMembre { get; set; } = string.Empty;
		public string UtilisateurLogin { get; set; } = string.Empty;
		
		public int AgenceId { get; set; } 
		public int MembreId { get; set; }
		public int NombreEcheancePaye { get; set; }
		public int NombreEcheanceImpaye { get; set; }
		public string DateDerniereEcheance { get; set; } = string.Empty;
    }
}