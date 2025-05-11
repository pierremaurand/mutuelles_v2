using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class MouvementRepository : IMouvementRepository
    {
        public readonly DataContext dc;
        public MouvementRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Mouvement mouvement)
        {
            if (dc.Mouvements is not null && mouvement is not null)
            {
                dc.Mouvements.Add(mouvement);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Mouvement>?> GetAllAsync()
        {
            if(dc.Mouvements is not null) {
                var mouvements = await dc.Mouvements
                .ToListAsync();
                if(mouvements is not null) {
                    return mouvements;
                }
            }

            return null;
        }
    }
}