using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.dtos
{
    public class CotisationDto
    {
        public int Id { get; set; }
        public int MembreId { get; set; }
        public string DateCotisation { get; set; } = string.Empty;
        public float Montant { get; set; }
    }
}