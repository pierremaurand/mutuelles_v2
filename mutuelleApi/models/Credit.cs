

using System.ComponentModel.DataAnnotations;

namespace mutuelleApi.models
{
    public class Credit : BaseEntity
    {
        public int MembreId { get; set; }
        public int Duree { get; set; }
        public double MontantCapital { get; set; }
        public double MontantCommission { get; set; }
        public double MontantInterets { get; set; }
        public string DateDemande { get; set; } = string.Empty;
        public string DateDecaissement { get; set; } = string.Empty;

        public List<Echeance>? Echeancier { get; set; }
        public List<Mouvement>? Mouvements { get; set; }
        public Membre? Membre { get; set; }
		
		
		

        public double MontantTotal => (MontantCapital + MontantCommission + MontantInterets);

        public double MontantRestant => (Echeancier?.Sum(m => m.MontantRestant) ?? 0);

        public string Status => MontantRestant == 0 ? "Soldé" : "En cour";

        public void Decaisser(int modificateur)
        {
            if (!string.IsNullOrEmpty(DateDecaissement) && Mouvements is null)
            {
				Mouvements = new List<Mouvement>();
                var mouvement = new Mouvement();
                mouvement.DateMouvement = DateDecaissement;
                mouvement.Libelle = "Décaissement " + Libelle;
                mouvement.MontantDebit = MontantCapital;
                mouvement.MembreId = MembreId;
                mouvement.ModifiePar = modificateur;
                mouvement.ModifieLe = DateTime.Now;
                Mouvements.Add(mouvement);
            }
        }

        public string Libelle => "crédit de " + MontantCapital + " du membre " + Membre?.Nom + " du " + DateDemande + " remboursable en " + Duree + " mois";
		
		public string NomMembre => Membre?.Nom ?? "";
		public string SexeMembre => Membre?.NomSexe ?? "";
		public string PhotoMembre => Membre?.Photo ?? "";
		public int AgenceId => Membre?.AgenceId ?? 0;
    }
}