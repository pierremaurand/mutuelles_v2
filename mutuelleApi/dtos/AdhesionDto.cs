namespace mutuelleApi.dtos
{
    public class AdhesionDto
    {
        public int Id { get;  set; }
        public int MembreId { get; set; } 
        public double Montant { get; set; } 
		public string DateAdhesion { get; set; } = string.Empty;
		public string NomMembre { get; set; } = string.Empty;
		public string SexeMembre { get; set; } = string.Empty;
		public string PhotoMembre { get; set; } = string.Empty;
		public int AgenceId { get; set; } 
    }
}