using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class AgenceRepository(DataContext dc) : IAgenceRepository
    {
        public readonly DataContext dc = dc;

        public void Add(Agence agence)
        {
            if (dc.Agences is not null && agence is not null)
            {
                dc.Agences.Add(agence);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Agence>?> FindAllAsync()
        {
            if (dc.Agences is not null)
            {
                var agences = await dc.Agences
                .ToListAsync();
                if (agences is not null)
                {
                    return agences;
                }
            }

            return null;
        }

        public async Task<Agence?> FindByIdAsync(int id)
        {
            if (dc.Agences is not null)
            {
                var agence = await dc.Agences
                .Where(s => s.Id == id)
                .FirstAsync();
                if(agence is not null)
                {
                    return agence;
                }
            }

            return null;
        }

    }
}