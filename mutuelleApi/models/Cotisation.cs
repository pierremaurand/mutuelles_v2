

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Cotisation : BaseEntity
    {
        public int MembreId { get; set; }
        public string DateCotisation { get; set; } = string.Empty;
        public double Salaire { get; set; } = 0;
        public double Retenue => Math.Round(Salaire * 0.05);

        public Membre? Membre { get; set; }
        public Mouvement? Mouvement { get; set; }

        
        public string Libelle => "cotisation du membre " + Membre?.Nom + " du " + DateCotisation;
        
        public void Payer(int modificateur)
        {
            if (!string.IsNullOrEmpty(DateCotisation) && Mouvement is null)
            {
                var mouvement = new Mouvement();
                mouvement.DateMouvement = DateCotisation;
                mouvement.Libelle = "Paiement " + Libelle;
                mouvement.MontantCredit = Retenue;
                mouvement.MembreId = MembreId;
                mouvement.ModifiePar = modificateur;
                mouvement.ModifieLe = DateTime.Now;
                Mouvement = mouvement;
            }
        }
		
		public string NomMembre => Membre?.Nom ?? "";
		public string PhotoMembre => Membre?.Photo ?? "";
    }
}