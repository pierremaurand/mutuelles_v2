namespace mutuelleApi.dtos
{
    public class InfosAvanceDto
    {
        public AvanceDto Avance {get; set;} = new AvanceDto();
		public List<EcheanceAvanceRequestDto> Echeancier {get; set; } = new List<EcheanceAvanceRequestDto>();
    }
}