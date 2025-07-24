using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Avance : BaseEntity
    {
        public int MembreId { get; set; }
        public int Duree { get; set; }
        public float MontantCapital { get; set; }
        public string DateDemande { get; set; } = string.Empty;
        public string DateDecaissement { get; set; } = string.Empty;
    }
}