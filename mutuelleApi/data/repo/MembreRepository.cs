using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class MembreRepository(DataContext dc) : IMembreRepository
    {
        public readonly DataContext dc = dc;

        public void Add(Membre membre)
        {
            if (dc.Membres is not null && membre is not null)
            {
                dc.Membres.Add(membre);
            }
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Membre>?> FindAllAsync()
        {
            if (dc.Membres is not null)
            {
                var membres = await dc.Membres
                .ToListAsync();
                if (membres is not null)
                {
                    return membres;
                }
            }

            return null;
        }

        public async Task<Membre?> FindByIdAsync(int id)
        {
            if (dc.Membres is not null)
            {
                var membre = await dc.Membres
                .Where(s => s.Id == id)
                .FirstAsync();
                if (membre is not null)
                {
                    return membre;
                }
            }

            return null;
        }
    }
}