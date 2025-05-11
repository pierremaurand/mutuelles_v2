using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IUtilisateurRepository
    {
        Task<IEnumerable<Utilisateur>?> GetAllAsync();
        void Add(Utilisateur utilisateur);
        void Delete(int id);
    }
}