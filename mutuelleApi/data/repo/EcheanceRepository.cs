using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class EcheanceRepository(DataContext dc) : IEcheanceRepository
    {
        public readonly DataContext dc = dc;

        public void Add(Echeance echeance)
        {
            if(dc.Echeances is not null && echeance is not null) {
                dc.Echeances.Add(echeance);
            }
        }

        public void Delete(int id)
        {
            if(dc.Echeances is not null) {
				var echeance = dc.Echeances.Find(id);
				if(echeance is not null) {
					dc.Echeances.Remove(echeance);
				}
            }
        }

        public async Task<IEnumerable<Echeance>?> GetAllAsync()
        {
            if(dc.Echeances is not null) {
                var echeances = await dc.Echeances
				.Include(c => c.Credit)
				.Include(c => c.Avance)
				.Include(c => c.Mouvements)
				.Include(c => c.Utilisateur)
                .ToListAsync();
                if(echeances is not null) {
                    return echeances;
                }
            }

            return null;
        }
		
		public async Task<Echeance?> GetByIdAsync(int id)
        {
            if(dc.Echeances is not null) {
                var echeance = await dc.Echeances
				.Include(c => c.Credit)
				.Include(c => c.Avance)
				.Include(c => c.Mouvements)
				.Include(c => c.Utilisateur)
                .Where(s => s.Id == id)
                .FirstAsync();
                if(echeance is not null) {
                    return echeance;
                }
            }

            return null;
        }
    }
}