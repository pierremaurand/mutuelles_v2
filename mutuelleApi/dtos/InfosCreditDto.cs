namespace mutuelleApi.dtos
{
    public class InfosCreditDto
    {
        public CreditDto Credit {get; set;} = new CreditDto();
		public List<EcheanceCreditRequestDto> Echeancier {get; set; } = new List<EcheanceCreditRequestDto>();
    }
}