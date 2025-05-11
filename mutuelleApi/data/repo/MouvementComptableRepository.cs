using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class MouvementComptableRepository : IMouvementComptableRepository
    {
        public readonly DataContext dc;
        public MouvementComptableRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(MouvementComptable mouvement)
        {
            if (dc.MouvementComptables is not null && mouvement is not null)
            {
                dc.MouvementComptables.Add(mouvement);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MouvementComptable>?> GetAllAsync()
        {
             if(dc.MouvementComptables is not null) {
                var mouvementcomptables = await dc.MouvementComptables
                .ToListAsync();
                if(mouvementcomptables is not null) {
                    return mouvementcomptables;
                }
            }

            return null;
        }
    }
}