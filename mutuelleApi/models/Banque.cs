using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace mutuelleApi.models
{
    public class Banque: BaseEntity
    {
        public string Nom { get; set; } = string.Empty;
        public List<Mouvement>? Mouvements { get; set; }  

        [ForeignKey("ModifiePar")]
		public Utilisateur? Utilisateur { get; set; }
		
		public string UtilisateurLogin => Utilisateur?.Login ?? "";
    }
}