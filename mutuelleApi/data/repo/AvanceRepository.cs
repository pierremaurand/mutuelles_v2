using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class AvanceRepository : IAvanceRepository
    {
        public readonly DataContext dc;
        public AvanceRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Avance avance)
        {
            if(dc.Avances is not null && avance is not null) {
                dc.Avances.Add(avance);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Avance>?> GetAllAsync()
        {
             if(dc.Avances is not null) {
                var avances = await dc.Avances
                .ToListAsync();
                if(avances is not null) {
                    return avances;
                }
            }

            return null;
        }
    }
}