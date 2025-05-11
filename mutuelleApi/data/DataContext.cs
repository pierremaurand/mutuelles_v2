using Microsoft.EntityFrameworkCore;
using mutuelleApi.models;

namespace mutuelleApi.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Agence>? Agences { get; set; }
        public DbSet<Avance>? Avances { get; set; }
        public DbSet<Compte>? Comptes { get; set; }
        public DbSet<Cotisation>? Cotisations { get; set; }
        public DbSet<Credit>? Credits { get; set; }
        public DbSet<Echeance>? Echeances { get; set; }
        public DbSet<Ecriture>? Ecritures { get; set; }
        public DbSet<Gabarit>? Gabarits { get; set; }
        public DbSet<Membre>? Membres { get; set; }
        public DbSet<Mouvement>? Mouvements { get; set; }
        public DbSet<MouvementComptable>? MouvementComptables { get; set; }
        public DbSet<Operation>? Operations { get; set; }
        public DbSet<Utilisateur>? Utilisateurs { get; set; }
    }
}