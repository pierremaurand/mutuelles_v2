using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class EcheanceRepository : IEcheanceRepository
    {
        public readonly DataContext dc;
        public EcheanceRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Echeance echeance)
        {
            if(dc.Echeances is not null && echeance is not null) {
                dc.Echeances.Add(echeance);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Echeance>?> GetAllAsync()
        {
            if(dc.Echeances is not null) {
                var echeances = await dc.Echeances
                .ToListAsync();
                if(echeances is not null) {
                    return echeances;
                }
            }

            return null;
        }
    }
}