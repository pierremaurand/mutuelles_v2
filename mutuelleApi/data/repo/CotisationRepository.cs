using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class CotisationRepository : ICotisationRepository
    {
        public readonly DataContext dc;
        public CotisationRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Cotisation cotisation)
        {
            if(dc.Cotisations is not null && cotisation is not null) {
                dc.Cotisations.Add(cotisation);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cotisation>?> GetAllAsync()
        {
            if(dc.Cotisations is not null) {
                var cotisations = await dc.Cotisations
                .ToListAsync();
                if(cotisations is not null) {
                    return cotisations;
                }
            }

            return null;
        }
    }
}