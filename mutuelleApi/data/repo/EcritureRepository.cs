using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class EcritureRepository : IEcritureRepository
    {
        public readonly DataContext dc;
        public EcritureRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Ecriture ecriture)
        {
            if(dc.Ecritures is not null && ecriture is not null) {
                dc.Ecritures.Add(ecriture);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Ecriture>?> GetAllAsync()
        {
            if(dc.Ecritures is not null) {
                var ecritures = await dc.Ecritures
                .ToListAsync();
                if(ecritures is not null) {
                    return ecritures;
                }
            }

            return null;
        }
    }
}