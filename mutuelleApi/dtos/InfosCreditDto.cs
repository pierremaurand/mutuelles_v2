namespace mutuelleApi.dtos
{
    public class InfosCreditDto
    {
        public CreditDto Credit {get; set;} = new CreditDto();
		public List<EcheanceDto> Echeancier {get; set; } = new List<EcheanceDto>();
    }
}