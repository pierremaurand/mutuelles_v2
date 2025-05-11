using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IMouvementComptableRepository
    {
        Task<IEnumerable<MouvementComptable>?> GetAllAsync();
        void Add(MouvementComptable mouvement);
        void Delete(int id);
    }
}