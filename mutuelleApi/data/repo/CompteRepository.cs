using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class CompteRepository : ICompteRepository
    {
        public readonly DataContext dc;
        public CompteRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Compte compte)
        {
            if(dc.Comptes is not null && compte is not null) {
                dc.Comptes.Add(compte);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Compte>?> GetAllAsync()
        {
            if(dc.Comptes is not null) {
                var comptes = await dc.Comptes
                .ToListAsync();
                if(comptes is not null) {
                    return comptes;
                }
            }

            return null;
        }
    }
}