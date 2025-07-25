

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Cotisation : BaseEntity
    {
        [Required]
        public int MembreId { get; set; }
        [Required]
        public string DateCotisation { get; set; } = string.Empty;
		[Required]
        public float Salaire { get; set; }
        [Required]
        public float Retenue { get{
			return Round(Salaire*0.05);
			}
    }
}