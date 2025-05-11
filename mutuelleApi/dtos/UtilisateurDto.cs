using mutuelleApi.enums;

namespace mutuelleApi.dtos
{
    public class UtilisateurDto
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public Role? Role { get; set; }
        public bool? EstActif { get; set; }
        public string? Photo { get; set; }
    }
}