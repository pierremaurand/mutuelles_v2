using Microsoft.EntityFrameworkCore;
using mutuelleApi.interfaces;
using mutuelleApi.models;

namespace mutuelleApi.data.repo
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        public readonly DataContext dc;
        public UtilisateurRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void Add(Utilisateur utilisateur)
        {
            if (dc.Utilisateurs is not null && utilisateur is not null)
            {
                dc.Utilisateurs.Add(utilisateur);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Utilisateur>?> GetAllAsync()
        {
            if(dc.Utilisateurs is not null) {
                var utilisateurs = await dc.Utilisateurs
                .ToListAsync();
                if(utilisateurs is not null) {
                    return utilisateurs;
                }
            }

            return null;
        }
    }
}