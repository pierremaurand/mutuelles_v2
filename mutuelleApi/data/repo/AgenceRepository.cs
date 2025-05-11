using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class AgenceRepository : IAgenceRepository
    {
        public readonly DataContext dc;
        public AgenceRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Agence agence)
        {
            if(dc.Agences is not null && agence is not null) {
                dc.Agences.Add(agence);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Agence>?> GetAllAsync()
        {
            if(dc.Agences is not null) {
                var agences = await dc.Agences
                .ToListAsync();
                if(agences is not null) {
                    return agences;
                }
            }

            return null;
        }
    }
}