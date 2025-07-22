using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class CotisationRepository(DataContext dc) : ICotisationRepository
    {
        public readonly DataContext dc = dc;

        public void Add(Cotisation cotisation)
        {
            if (dc.Cotisations is not null && cotisation is not null)
            {
                dc.Cotisations.Add(cotisation);
            }
        }

        public void Delete(int id)
        {
            if (dc.Cotisations is not null)
            {
                var cotisation = dc.Cotisations.Find(id); 
				if(cotisation != null) {
					dc.Cotisations.Remove(cotisation);
				}
            }
        }

        public async Task<IEnumerable<Cotisation>?> GetAllAsync()
        {
            if (dc.Cotisations is not null)
            {
                var cotisations = await dc.Cotisations
                .ToListAsync();
                if (cotisations is not null)
                {
                    return cotisations;
                }
            }

            return null;
        }
        
        public async Task<Cotisation?> GetByIdAsync(int id)
        {
            if(dc.Cotisations is not null) {
                var cotisation = await dc.Cotisations
                .Where(x => x.Id == id)
                .FirstAsync();
                if (cotisation is not null) {
                    return cotisation;
                }
            }

            return null;
        }
    }
}