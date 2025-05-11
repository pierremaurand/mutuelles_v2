using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface ICompteRepository
    {
        Task<IEnumerable<Compte>?> GetAllAsync();
        void Add(Compte compte);
        void Delete(int id);
    }
}