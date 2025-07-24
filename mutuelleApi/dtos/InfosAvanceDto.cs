namespace mutuelleApi.dtos
{
    public class InfosAvanceDto
    {
        public AvanceDto Avance {get; set;} = new AvanceDto();
		public List<EcheanceDto> Echeancier {get; set; } = new List<EcheanceDto>();
    }
}