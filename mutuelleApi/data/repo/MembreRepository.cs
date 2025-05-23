using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class MembreRepository : IMembreRepository
    {
        public readonly DataContext dc;
        public MembreRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Membre membre)
        {
            if (dc.Membres is not null && membre is not null)
            {
                dc.Membres.Add(membre);
            }
        }

        public async Task<bool> AgenceIsUse(int id)
        {
            if (dc.Membres is not null)
            {
                return await dc.Membres.AnyAsync(x => x.AgenceId == id);
            }
            return false;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Membre>?> GetAllAsync()
        {
             if(dc.Membres is not null) {
                var membres = await dc.Membres
                .ToListAsync();
                if(membres is not null) {
                    return membres;
                }
            }

            return null;
        }

        public async Task<bool> UtilisateurIsUse(int id)
        {
            if (dc.Membres is not null)
            {
                return await dc.Membres.AnyAsync(x => x.ModifiePar == id);
            }
            return false;
        }
    }
}