using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IMouvementRepository
    {
        Task<IEnumerable<Mouvement>?> GetAllAsync();
        void Add(Mouvement mouvement);
        void Delete(int id);
    }
}