using mutuelleApi.models;

namespace mutuelleApi.interfaces
{
    public interface IEcritureRepository
    {
        Task<IEnumerable<Ecriture>?> GetAllAsync();
        void Add(Ecriture ecriture);
        void Delete(int id);
    }
}