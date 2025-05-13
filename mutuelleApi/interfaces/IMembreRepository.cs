using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IMembreRepository
    {
        Task<IEnumerable<Membre>?> GetAllAsync();
        void Add(Membre membre);
        void Delete(int id);
        Task<bool> AgenceIsUse(int id);
        Task<bool> UtilisateurIsUse(int id);
    }
}