using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class GabaritRepository(DataContext dc) : IGabaritRepository
    {
        public readonly DataContext dc = dc;

        public void Add(Gabarit gabarit)
        {
            if(dc.Gabarits is not null && gabarit is not null) {
                dc.Gabarits.Add(gabarit);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Gabarit>?> GetAllAsync()
        {
            if(dc.Gabarits is not null) {
                var gabarits = await dc.Gabarits
                .ToListAsync();
                if(gabarits is not null) {
                    return gabarits;
                }
            }

            return null;
        }
    }
}